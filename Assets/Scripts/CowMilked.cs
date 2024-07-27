using UnityEngine;
using UnityEngine.UI;

public class CowMilked : MonoBehaviour
{
    public EntitiesManager entitiesManager;
    public Slider staminaSlider;
    public float cowValueMultiplier = 10f;

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

        UpdateStaminaSlider();
    }

    public void UpdateStaminaSlider()
    {
        if (entitiesManager != null && staminaSlider != null)
        {
            int numberOfCows = entitiesManager.cows.Count;
            float newSliderValue = numberOfCows * cowValueMultiplier;
            staminaSlider.value = newSliderValue;
        }
    }
}
