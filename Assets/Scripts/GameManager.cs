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
    public bool pauseMenu;
    public GameObject pauseMenuObject;
    public GameObject winMenu;
    public GameObject gameoverMenu;
    public Animator fadePanel;


    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        mainCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        flowerCount = 0;

        fadePanel.Play("FadeOut");
        pauseMenuObject.SetActive(false);
        pauseMenu = false;
        gameoverMenu.SetActive(false);
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

            foreach (var cow in entitiesManager.cows)
            {
                cow.GetComponent<CowBoids>().followQueen = false;
                cow.GetComponent<CowBoids>().target = endQueenTarget;
            }

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
        PauseTime();
        winMenu.SetActive(true);
    }

    public void Lose()
    {
        if (!gameOver)
        {
            gameOver = true;
            PauseTime();
            gameoverMenu.SetActive(true);
        }
    }

    public void PauseMenu()
    {
        if (pauseMenu)
        {
            pauseMenu = false;
            pauseMenuObject.SetActive(false);
            ResumeTime();
        }
        else
        {
            pauseMenu = true;
            pauseMenuObject.SetActive(true);
            PauseTime();
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

    public void ScreenFadeOut()
    {
        fadePanel.Play("FadeIn");
    }
}
