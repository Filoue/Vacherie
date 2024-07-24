using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int flowerCount;
    [SerializeField] private int winFlowerCount;
    [SerializeField] private Transform flowerPanel;
    [SerializeField] private GameObject UIFlowerPrefab;

    private void Start()
    {
        flowerCount = 0;
    }

    private void Update()
    {
        if (flowerCount >= winFlowerCount)
        {
            FinishLevel();
        }
    }

    public void GainFlower()
    {
        flowerCount++;
        Instantiate(UIFlowerPrefab, flowerPanel);
    }

    private void FinishLevel()
    {

    }

    private void Win()
    {

    }

    private void Die()
    {

    }
}
