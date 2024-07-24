using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cow" || other.tag == "Queen")
        {
            Collect();
        }
    }

    private void Collect()
    {
        gameManager.GainFlower();
        Destroy(gameObject);
    }
}
