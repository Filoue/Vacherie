using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowMilked : MonoBehaviour
{
    public EntitiesManager entitiesManager;
    public Slider staminaSlider;
    public float milkValueMultiplier = 10f; // Configurable value for the score

    // Variables for saving the number of cows and spawning configuration
    private int savedNumberOfCows;
    public Vector3 spawnPosition;
    public float spacing = 2f; // Configurable spacing between cows


    public GameObject cowPrefab;
    public MilkFill milkFill;

    void Start()
    {
        if (entitiesManager == null)
        {
            entitiesManager = FindObjectOfType<EntitiesManager>();
        }

        if (staminaSlider == null)
        {
            staminaSlider = GetComponentInChildren<Slider>();
        }

        if (milkFill == null)
        {
            milkFill = FindObjectOfType<MilkFill>();
        }

        UpdateStaminaSlider();
        SaveNumberOfCows();
    }

    public void Initialize()
    {
        if (entitiesManager == null)
        {
            entitiesManager = FindObjectOfType<EntitiesManager>();
        }

        if (staminaSlider == null)
        {
            staminaSlider = GetComponentInChildren<Slider>();
        }

        if (milkFill == null)
        {
            milkFill = FindObjectOfType<MilkFill>();
        }

        UpdateStaminaSlider();
        SaveNumberOfCows();
    }

    public void UpdateStaminaSlider()
    {
        if (entitiesManager != null && staminaSlider != null)
        {
            int numberOfCows = entitiesManager.cows.Count;
            float newSliderValue = numberOfCows * milkValueMultiplier;
            staminaSlider.value = newSliderValue;
            milkFill.SetTargetValue(newSliderValue); // Set target value for MilkFill
        }
    }

    public void SaveNumberOfCows()
    {
        if (entitiesManager != null)
        {
            savedNumberOfCows = entitiesManager.cows.Count;
        }
    }

    public void SpawnCows()
    {
        if (cowPrefab != null)
        {
            for (int i = 0; i < savedNumberOfCows; i++)
            {
                Vector3 position = spawnPosition + new Vector3(i * spacing, 0, 0);
                Instantiate(cowPrefab, position, Quaternion.identity);
            }
        }
    }
}
