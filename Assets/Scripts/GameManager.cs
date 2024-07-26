using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int flowerCount;
    [SerializeField] private Transform endDogTarget;
    [SerializeField] private Transform endQueenTarget;
    [SerializeField] private Transform endCameraTarget;
    [SerializeField] private float finishTimer;
    [SerializeField] private Transform flowerPanel;
    [SerializeField] private GameObject UIFlowerPrefab;
    private EntitiesManager entitiesManager;
    private CameraFollow mainCameraScript;
    private bool gameOver;
    private bool finishing;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        mainCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        flowerCount = 0;
    }

    private void Update()
    {
        if (finishing)
        {
            foreach (var dog in entitiesManager.dogs)
            {
                dog.GetComponent<DogAI>().GoToTarget(endDogTarget.transform.position);
            }
        }
    }

    public void GainFlower()
    {
        flowerCount++;
        Instantiate(UIFlowerPrefab, flowerPanel);
    }

    public void FinishLevel()
    {
        if (!finishing)
        {
            finishing = true;
            entitiesManager.queen.GetComponent<CowBoids>().target = endQueenTarget;
            mainCameraScript.newTarget(endCameraTarget, true);
            endCameraTarget.GetComponent<LerpPosition>().StartMoving();
            Invoke("Win", finishTimer);
        }
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
