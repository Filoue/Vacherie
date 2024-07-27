using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int flowerCount;
    [SerializeField] private Transform endDogTarget;
    [SerializeField] private Transform endQueenTarget;
    [SerializeField] private Transform endCameraTarget;
    [SerializeField] private float finishTimer;
    [SerializeField] private Transform flowerPanel;
    [SerializeField] private Transform winFlowerPanel;
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
    private SoundManager soundManager;
    public TextMeshProUGUI scoreText;
    public Transform killLimit;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        mainCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        flowerCount = 0;

        fadePanel.Play("FadeOut");
        pauseMenuObject.SetActive(false);
        pauseMenu = false;
        gameoverMenu.SetActive(false);
        winMenu.SetActive(false);
        soundManager = GameObject.FindWithTag("Travel").GetComponent<SoundManager>();

        soundManager.PlayGameMusic();
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
        Instantiate(UIFlowerPrefab, winFlowerPanel);
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
        soundManager.PlayEndMusic();
        winMenu.SetActive(true);
        SetScore();
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
        soundManager.ToggleAmbient();
        soundManager.ToggleEntities();
    }

    private void ResumeTime()
    {
        Time.timeScale = 1;
        soundManager.ToggleAmbient();
        soundManager.ToggleEntities();
    }

    public void ScreenFadeOut()
    {
        fadePanel.Play("FadeIn");
    }

    public void SetScore()
    {
        int removeToScore = 0;
        for (int i = 0; i < entitiesManager.cows.Count; i++)
        {
            if (entitiesManager.cows[i].transform.position.z < killLimit.position.z)
            {
                removeToScore++;
            }
        }

        scoreText.text = (entitiesManager.cows.Count - removeToScore).ToString();
    }
}
