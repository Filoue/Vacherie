using UnityEngine;

public class MilkFill : MonoBehaviour
{
    public ParticleSystem Milk;
    private ParticleSystem.EmissionModule emissionModule;

    [Range(0, 1)]
    public float fillAmount;

    private void Start()
    {
        emissionModule = Milk.emission;
        SetFillAmount(fillAmount);
    }

    private void Update()
    {
        SetFillAmount(fillAmount);
    }

    public void SetFillAmount(float fillAmount)
    {

        fillAmount = Mathf.Clamp01(fillAmount);


        emissionModule.rateOverTime = fillAmount * 100;
    }
}
