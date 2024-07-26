using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    public GameObject spawnObject; // The object to spawn
    public LayerMask Road; // Layer mask to identify the road
    public float spawnInterval = 0.5f; // Time interval between spawns
    public float raycastDistance = 100.0f; // Distance for the raycast

    private float lastSpawnTime;

    void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            if (IsOnRoad())
            {
                SpawnObject();
                lastSpawnTime = Time.time;
            }
        }
    }

    bool IsOnRoad()
    {
        RaycastHit hit;
        // Cast a ray downwards to check if the cow is on the road
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, Road))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            return hit.collider != null;
        }
        Debug.Log("Raycast did not hit anything.");
        return false;
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned object at position: " + spawnPosition);
    }
}
