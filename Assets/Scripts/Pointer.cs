using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject visuals;

    private EntitiesManager entitiesManager;
    private float nearestDogDistance;
    public float dissapearDistance;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        visuals.SetActive(false);
    }

    private void Update()
    {
        if (NearestDogDistance(transform.position) < dissapearDistance)
        {
            visuals.SetActive(false);
        }
    }

    public void MovePointerTo(Vector3 position)
    {
        visuals.SetActive(true);
        transform.position = position;
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
}