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
    public bool finishing;
    private bool pauseMenu;
    public GameObject PauseMenuObject;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        mainCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        flowerCount = 0;
    }

    private void Update()
    {
        if (entitiesManager.queen == null || entitiesManager.dogs.Count <= 0)
        {
            Lose();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
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

            foreach (var dog in entitiesManager.dogs)
            {
                DogAI dogAI = dog.GetComponent<DogAI>();
                dogAI.GoToTarget(endDogTarget.transform.position);
                dogAI.neigbourgRange = 0;
            }
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

    private void PauseMenu()
    {
        if (pauseMenu)
        {
            pauseMenu = false;
            PauseTime();
        }
        else
        {
            pauseMenu = true;
            ResumeTime();
        }
    }

    private void PauseTime()
    {
        Time.timeScale = 0;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
