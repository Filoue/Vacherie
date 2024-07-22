using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CowsManager : MonoBehaviour
{
    public List<GameObject> cows;

    private void Start()
    {
        cows = GameObject.FindGameObjectsWithTag("Cow").ToList();
    }
}
