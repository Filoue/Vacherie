using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int flowerCount;
    [SerializeField] private int winFlowerCount;
    [SerializeField] private Transform endTarget;
    [SerializeField] private Transform endCameraTarget;
    [SerializeField] private float finishTimer;
    [SerializeField] private Transform flowerPanel;
    [SerializeField] private GameObject UIFlowerPrefab;
    private EntitiesManager entitiesManager;
    private CameraFollow mainCameraScript;
    private bool gameOver;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        mainCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
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

    public void FinishLevel()
    {
        print("Finished level");
        entitiesManager.queen.GetComponent<CowBoids>().target = endTarget;
        mainCameraScript.newTarget(endCameraTarget);
        endCameraTarget.GetComponent<LerpPosition>().StartMoving();
    }

    private void Win()
    {

    }

    public void Lose()
    {
        if (!gameOver)
        {
            print("Lol you lost... skill issue");
            gameOver = true;
        }
    }
}
