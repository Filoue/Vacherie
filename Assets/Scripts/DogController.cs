using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour
{
    public float speed = 5f; // Dog speed
    public float curveOffset = 2f; // Offset of the curved path
    public float exclusionRadius = 2f; // Radius where repeated clicks are ignored for X seconds
    public float exclusionDuration = 1f;
    public float smallExclusionRadius = 0.5f; // Smaller radius clicked dog's position - prevents other dogs from moving there

    private Vector3 lastClickPosition = Vector3.positiveInfinity;
    private float exclusionTimer = 0f;
    private bool isExclusionActive = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = GetMouseWorldPosition();

            if (isExclusionActive && IsWithinExclusionRadius(targetPosition))
            {
                Debug.Log("Click around already set goal, ignoring.");
                return;
            }

            GameObject clickedDog = GetDogWithinSmallExclusionRadius(targetPosition);
            if (clickedDog != null)
            {
                PlayExclusionSound(clickedDog);
                Debug.Log("Click within around the dog, playing Halètement.");
                return;
            }

            lastClickPosition = targetPosition;
            isExclusionActive = true;
            exclusionTimer = exclusionDuration;
            ProcessClick(targetPosition);
        }

        if (isExclusionActive)
        {
            exclusionTimer -= Time.deltaTime;
            if (exclusionTimer <= 0f)
            {
                isExclusionActive = false;
                Debug.Log("Goal radius expired, reset.");
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return new Vector3(worldPosition.x, worldPosition.y, 0f);
    }

    private bool IsWithinExclusionRadius(Vector3 targetPosition)
    {
        return Vector3.Distance(lastClickPosition, targetPosition) <= exclusionRadius;
    }

    private GameObject GetDogWithinSmallExclusionRadius(Vector3 targetPosition)
    {
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        foreach (GameObject dog in dogs)
        {
            if (Vector3.Distance(dog.transform.position, targetPosition) <= smallExclusionRadius)
            {
                return dog;
            }
        }
        return null;
    }

    private void PlayExclusionSound(GameObject dog)
    {
        AudioSource[] audioSources = dog.GetComponents<AudioSource>();
        if (audioSources.Length > 1)
        {
            AudioSource exclusionAudioSource = audioSources[1]; // Play "halètement"
            if (exclusionAudioSource != null)
            {
                exclusionAudioSource.Play();
            }
        }
    }

    private void ProcessClick(Vector3 targetPosition)
    {
        Debug.Log($"Target position: {targetPosition}");
        GameObject closestDog = FindClosestDog(targetPosition);

        if (closestDog != null)
        {
            DogMovement dogMovement = closestDog.GetComponent<DogMovement>();
            if (dogMovement != null && !dogMovement.isMoving)
            {
                Debug.Log($"Dog {closestDog.name} starts going there.");
                dogMovement.isMoving = true;
                AudioSource audioSource = closestDog.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                Vector3 controlPoint = GenerateControlPoint(closestDog.transform.position, targetPosition);
                StartCoroutine(MoveDogAlongBezierCurve(dogMovement, closestDog.transform.position, controlPoint, targetPosition));
            }
        }
        else
        {
            Debug.Log("No closest dog found.");
        }
    }

    private GameObject FindClosestDog(Vector3 targetPosition)
    {
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        GameObject closestDog = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject dog in dogs)
        {
            DogMovement dogMovement = dog.GetComponent<DogMovement>();
            if (dogMovement != null && !dogMovement.isMoving)
            {
                Vector3 dogPosition = dog.transform.position;
                Debug.Log($"Dog {dog.name} position: {dogPosition}");
                float distance = Vector3.Distance(dogPosition, targetPosition);
                Debug.Log($"Dog {dog.name} distance to target: {distance}");
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDog = dog;
                }
            }
        }

        if (closestDog != null)
        {
            Debug.Log($"Closest dog found: {closestDog.name} with distance: {closestDistance}");
        }

        return closestDog;
    }

    private Vector3 GenerateControlPoint(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector3 midPoint = (startPosition + targetPosition) / 2;
        Vector3 direction = (targetPosition - startPosition).normalized;
        Vector3 perpendicularDirection = Vector3.Cross(direction, Vector3.forward).normalized;
        return midPoint + perpendicularDirection * Random.Range(-curveOffset, curveOffset);
    }

    private IEnumerator MoveDogAlongBezierCurve(DogMovement dogMovement, Vector3 startPosition, Vector3 controlPoint, Vector3 endPosition)
    {
        GameObject dog = dogMovement.gameObject;
        float t = 0f;
        while (t < 1f)
        {
            t += speed * Time.deltaTime / Vector3.Distance(startPosition, endPosition);
            dog.transform.position = CalculateBezierPoint(t, startPosition, controlPoint, endPosition);
            yield return null;
        }
        Debug.Log($"Dog {dog.name} reached the goal. Waits for 1 second.");
        yield return new WaitForSeconds(1f); // Wait for 1 second
        dogMovement.isMoving = false;
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }
}
