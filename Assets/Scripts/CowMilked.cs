using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowMilked : MonoBehaviour
{
    public EntitiesManager entitiesManager;
    public Slider staminaSlider;
    public float milkValueMultiplier = 16.67f;
    public MilkFill milkFill;

    void Start()
    {
        Initialize();
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
}
