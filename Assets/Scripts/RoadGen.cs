using UnityEngine;

public class RoadGen : MonoBehaviour
{
    public GameObject[] RoadsL;
    public GameObject[] RoadsR;
    public int RoadCount = 6;

    private Vector3 currentTipPosition;
    private GameObject lastRoad;

    void Start()
    {
        if (RoadsL.Length == 0 || RoadsR.Length == 0)
        {
            Debug.LogError("One or + of road pools empty.");
            return;
        }

        lastRoad = gameObject;
        currentTipPosition = lastRoad.transform.position + GetRoadSize(lastRoad);

        for (int i = 0; i < RoadCount; i++)
        {
            AddRoad(i);
        }
    }

    void AddRoad(int index)
    {
        // Which pool to use based on the index
        GameObject[] currentPool = (index % 2 == 0) ? RoadsL : RoadsR;

        // Random road from pool
        GameObject randomRoadPrefab = currentPool[Random.Range(0, currentPool.Length)];

        // +1 road at tip
        GameObject newRoad = Instantiate(randomRoadPrefab, currentTipPosition, Quaternion.identity);
        Debug.Log("New road instantiated at position: " + currentTipPosition);

        // Update tip the new road
        currentTipPosition = newRoad.transform.position + GetRoadSize(newRoad);

        // Update last road
        lastRoad = newRoad;
    }

    Vector3 GetRoadSize(GameObject road)
    {
        Renderer renderer = road.GetComponent<Renderer>();
        if (renderer != null)
        {
            return new Vector3(0, 0, renderer.bounds.size.z);
        }
        else
        {
            return new Vector3(0, 0, road.transform.localScale.z);
        }
    }
}
