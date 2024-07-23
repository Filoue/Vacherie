using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Proceduraltree : MonoBehaviour
{
    public GameObject treePrefab; // Le prefab de l'arbre � placer
    public int numberOfTrees = 100; // Le nombre d'arbres � placer
    public float planeSize = 10f; // La taille du plane

    void Start()
    {
        PlaceTrees();
    }

    void PlaceTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // G�n�re des positions al�atoires sur le plane
            float xPosition = Random.Range(-planeSize / 2, planeSize / 2);
            float zPosition = Random.Range(-planeSize / 2, planeSize / 2);

            // Calcule la position de l'arbre
            Vector3 treePosition = new Vector3(xPosition, 3, zPosition);
            Rotate treeRotate = new Rotate(30);

            // Instantie l'arbre � la position calcul�e
            Instantiate(treePrefab, treePosition, Quaternion.identity);
            
        }
    }
}
