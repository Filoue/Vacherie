using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using TMPro;
using Unity.Mathematics;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Proceduraltree : MonoBehaviour
{
    public GameObject Parents;
    public GameObject treePrefab; // Le prefab de l'arbre à placer
    public int numberOfTrees = 100; // Le nombre d'arbres à placer
    public float planeSize = 10f; // La taille du plane
    public bool isIn;

    void Start()
    {
        PlaceTrees();
    }

    void PlaceTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // Génère des positions aléatoires sur le plane
            float xPosition = Random.Range(-planeSize / 2, planeSize / 2);
            float zPosition = Random.Range(-planeSize / 2, planeSize / 2);

            // Calcule la position de l'arbre
            Vector3 treePosition = new Vector3(xPosition, 0f, zPosition);

            Quaternion treeRotation = Quaternion.Euler(30f, 0f, 0f);

            // Instantie l'arbre à la position calculée

            Instantiate(treePrefab, treePosition, treeRotation, treePrefab.transform.parent = Parents.transform);
            
            if (isIn)
            {
                Destroy(treePrefab);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            isIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Road") && isIn == true) 
        {
            isIn = false;
        }
    }
}
