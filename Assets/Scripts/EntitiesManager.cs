using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public List<GameObject> cows;
    public List<GameObject> dogs;
    public List<GameObject> obstacles;

    private void Start()
    {
        cows = GameObject.FindGameObjectsWithTag("Cow").ToList();
        dogs = GameObject.FindGameObjectsWithTag("Dog").ToList();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle").ToList();
    }
}
