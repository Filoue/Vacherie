using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject visuals;
    public GameObject smokePoofPrefab;

    private EntitiesManager entitiesManager;
    public float dissapearDistance;
    public AudioSource audioSource;
    public AudioClip bell;
    public AudioClip poof;
    public List<AudioClip> yodel;
    private List<AudioClip> tempYodel;
    public float pitchChangeRange;
    public float dingCooldown;
    public float yodelDistance;

    private bool canDing;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        visuals.SetActive(false);
        canDing = true;

        tempYodel = new List<AudioClip>();
    }

    private void Update()
    {
        if (NearestDogDistance(transform.position) < dissapearDistance)
        {
            GameObject poofInstance = Instantiate(smokePoofPrefab, transform.position, Quaternion.identity);
            Destroy(poofInstance, 2f); // Adjust the delay as necessary
            visuals.SetActive(false);
            transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue); // Move the pointer far away
        }
    }

    public void MovePointerTo(Vector3 position)
    {
        visuals.SetActive(true);
        transform.position = position;

        if (canDing)
        {
            canDing = false;
            Invoke("CanDingAgain", dingCooldown);
            audioSource.pitch = Random.Range(1 - pitchChangeRange, 1 + pitchChangeRange);
            audioSource.PlayOneShot(bell);
            audioSource.pitch = Random.Range(1 - pitchChangeRange, 1 + pitchChangeRange);
            audioSource.PlayOneShot(poof);
            audioSource.pitch = 1;
            Yodel();
        }
    }

    private float NearestDogDistance(Vector3 point)
    {
        float nearestDistance = Mathf.Infinity;
        foreach (var dog in entitiesManager.dogs)
        {
            float dogDistance = Vector3.Distance(point, dog.transform.position);
            if (dogDistance < nearestDistance)
            {
                nearestDistance = dogDistance;
            }
        }

        return nearestDistance;
    }

    public void CanDingAgain()
    {
        canDing = true;
    }

    private void Yodel()
    {
        if (NearestDogDistance(transform.position) > yodelDistance)
        {
            if (tempYodel.Count > 0)
            {
                int random = Random.Range(0, tempYodel.Count - 1);
                audioSource.PlayOneShot(tempYodel[random]);
                tempYodel.RemoveAt(random);
            }
            else
            {
                foreach (var sound in yodel)
                {
                    tempYodel.Add(sound);
                }

                Yodel();
            }
        }
    }
}
