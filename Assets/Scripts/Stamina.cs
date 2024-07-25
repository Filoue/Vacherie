using UnityEngine;
using UnityEngine.UI;

public class Cow : MonoBehaviour
{
    public Slider staminaBar;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDepletionRate = 10f;

    void Start()
    {
        currentStamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = currentStamina;
        }
    }

    void Update()
    {
        if (currentStamina > 0)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            if (staminaBar != null)
            {
                staminaBar.value = currentStamina;
            }
        }
    }
}
