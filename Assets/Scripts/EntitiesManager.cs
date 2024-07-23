using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public List<GameObject> cows;
    public List<GameObject> dogs;
    public List<GameObject> obstacles;

    [SerializeField]
    private LayerMask raycastLayer;
    private Vector3 callPosition;

    private void Start()
    {
        cows = GameObject.FindGameObjectsWithTag("Cow").ToList();
        dogs = GameObject.FindGameObjectsWithTag("Dog").ToList();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle").ToList();
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
                NearestDog(callPosition).GetComponent<DogAI>().target = ToVector2(callPosition);
            }
        }
    }

    private GameObject NearestDog(Vector3 point)
    {
        GameObject nearestDog = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var dog in dogs)
        {
            if (Vector3.Distance(point, dog.transform.position) < nearestDistance)
            {
                nearestDog = dog;
            }
        }

        return nearestDog;
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
