using UnityEngine;
using System.Collections;

public class DogController : MonoBehaviour
{
    public float speed = 5f; // Dog speed
    public float curveOffset = 2f; // Offset of the curved path
    public float exclusionRadius = 2f; // Radius where repeated clicks are ignored for X seconds
    public float exclusionDuration = 1f;
    public float directMovementDistance = 3f; // Distance before curving to destination
    public GameObject flowerPrefab;

    private Vector3 lastClickPosition = Vector3.positiveInfinity;
    private float exclusionTimer = 0f;
    private bool isExclusionActive = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = GetMouseGroundPosition();

            if (isExclusionActive && IsWithinExclusionRadius(targetPosition))
            {
                Debug.Log("Click around already set goal, ignoring.");
                return;
            }

            GameObject clickedDog = GetClickedDog(targetPosition);
            if (clickedDog != null)
            {
                ToggleDogLock(clickedDog);
                return;
            }

            if (AllDogsLocked())
            {
                GameObject closestDog = FindClosestDog(targetPosition, true); // Unlock the closest dog if all are locked
                if (closestDog != null)
                {
                    ToggleDogLock(closestDog); // Unlock the dog
                    ProcessClick(targetPosition);
                    return;
                }
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

    private Vector3 GetMouseGroundPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        Vector3 worldPosition = Vector3.zero;
        if (groundPlane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }

    private bool IsWithinExclusionRadius(Vector3 targetPosition)
    {
        return Vector3.Distance(lastClickPosition, targetPosition) <= exclusionRadius;
    }

    private GameObject GetClickedDog(Vector3 targetPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 0.1f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Dog"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    private void ToggleDogLock(GameObject dog)
    {
        DogMovement dogMovement = dog.GetComponent<DogMovement>();
        if (dogMovement != null)
        {
            if (dogMovement.isLocked)
            {
                Debug.Log("Dog unlocked, playing barking sound.");
                dogMovement.isLocked = false;
                AudioSource[] audioSources = dog.GetComponents<AudioSource>();
                if (audioSources.Length > 1)
                {
                    audioSources[1].Stop(); // Stop halètement sound
                }
                if (audioSources.Length > 0)
                {
                    audioSources[0].Play(); // Play barking sound
                }
            }
            else
            {
                Debug.Log("Dog locked, playing halètement sound.");
                dogMovement.isLocked = true;
                AudioSource[] audioSources = dog.GetComponents<AudioSource>();
                if (audioSources.Length > 1)
                {
                    audioSources[1].loop = true;
                    audioSources[1].Play(); // Play halètement sound continuously
                }
            }
        }
    }

    private void ProcessClick(Vector3 targetPosition)
    {
        Debug.Log($"Target position: {targetPosition}");
        GameObject closestDog = FindClosestDog(targetPosition, false);

        if (closestDog != null)
        {
            DogMovement dogMovement = closestDog.GetComponent<DogMovement>();
            if (dogMovement != null && !dogMovement.isMoving && !dogMovement.isLocked)
            {
                Debug.Log($"Dog {closestDog.name} starts moving.");
                dogMovement.isMoving = true;
                Quaternion flowerRotation = Quaternion.Euler(90, 0, 0); // Rotate the flower to face upward
                GameObject flower = Instantiate(flowerPrefab, targetPosition, flowerRotation);
                StartCoroutine(MoveDog(closestDog, targetPosition, flower));
            }
        }
        else
        {
            Debug.Log("No closest dog found.");
        }
    }

    private GameObject FindClosestDog(Vector3 targetPosition, bool ignoreLock)
    {
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        GameObject closestDog = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject dog in dogs)
        {
            DogMovement dogMovement = dog.GetComponent<DogMovement>();
            if (dogMovement != null && !dogMovement.isMoving && (ignoreLock || !dogMovement.isLocked))
            {
                Vector3 dogPosition = dog.transform.position;
                float distance = Vector3.Distance(dogPosition, targetPosition);
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

    private bool AllDogsLocked()
    {
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        foreach (GameObject dog in dogs)
        {
            DogMovement dogMovement = dog.GetComponent<DogMovement>();
            if (dogMovement != null && !dogMovement.isLocked)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator MoveDog(GameObject dog, Vector3 targetPosition, GameObject flower)
    {
        DogMovement dogMovement = dog.GetComponent<DogMovement>();
        AudioSource barkingAudioSource = dog.GetComponents<AudioSource>()[0];
        barkingAudioSource.Play(); // Play barking sound when the dog starts moving

        while (Vector3.Distance(dog.transform.position, targetPosition) > 0.1f)
        {
            dog.transform.position = Vector3.MoveTowards(dog.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log($"Dog {dog.name} reached the goal.");
        dogMovement.isMoving = false;
        Destroy(flower, 0.01f);
    }
}
