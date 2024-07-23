using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralRoad : MonoBehaviour
{
    [SerializeField] GameObject Road;
    public int road = 100; // Le nombre d'arbres à placer
    public float planeSize = 10f; // La taille du plane
    // Start is called before the first frame update
    void Start()
    {
        
        FirstRoad();
        PlaceRoad();
    }

    private void PlaceRoad()
    {
        if () 
        { 
            return; 
        }
        float zPosition = Random.Range(-planeSize / 2, planeSize / 2);

        float xPosition = Random.Range(-planeSize / 2, planeSize / 2);


        Vector3 roadPoint = new Vector3(xPosition, 0, zPosition);

        Instantiate(Road, roadPoint, Quaternion.identity);
    }
    private void FirstRoad()
    {
        Instantiate(Road,new Vector3(0, 0, 0), Quaternion.identity);
    }
}
