using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public List<GameObject> cows;
    public List<GameObject> dogs;
    public List<GameObject> obstacles;
    public GameObject queen;
    public GameObject pointer;

    [SerializeField]
    private LayerMask raycastLayer;
    private Vector3 callPosition;

    private void Start()
    {
        queen = GameObject.FindGameObjectWithTag("Queen");
        cows = GameObject.FindGameObjectsWithTag("Cow").ToList();
        cows.Add(queen);
        dogs = GameObject.FindGameObjectsWithTag("Dog").ToList();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle").ToList();
        pointer = GameObject.FindGameObjectWithTag("Pointer");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayer))
            {
                callPosition = hit.point;
                NearestDog(callPosition).GetComponent<DogAI>().GoToTarget(callPosition);
                pointer.GetComponent<Pointer>().MovePointerTo(callPosition);
            }
        }
    }

    private GameObject NearestDog(Vector3 point)
    {
        GameObject nearestDog = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var dog in dogs)
        {
            float dogDistance = Vector3.Distance(point, dog.transform.position);
            if (dogDistance < nearestDistance)
            {
                nearestDog = dog;
                nearestDistance = dogDistance;
            }
        }

        return nearestDog;
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
