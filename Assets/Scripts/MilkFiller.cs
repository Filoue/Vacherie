using UnityEngine;
using UnityEngine.UI;

public class MilkFiller : MonoBehaviour
{
    public Slider MilkSlider;
    public float fillSpeed = 20f;
    public AudioSource Sound1;
    public AudioSource Sound2;
    public AudioSource Sound3;
    public AudioSource Sound4;
    public GameObject milkgnome;
    public GameObject smoePoofUIPrefab;

    private bool SoundPlayed1 = false;
    private bool SoundPlayed2 = false;
    private bool SoundPlayed3 = false;
    private bool SoundPlayed4 = false;

    private float targetValue;
    private bool filling = false;

    private RectTransform gnomeRectTransform;
    private RectTransform sliderRectTransform;

    private void Awake()
    {
        Debug.Log("MilkFiller Awake: Initialized.");
        Debug.Log($"MilkFiller Instance in Awake: {this.GetInstanceID()}");
    }

    private void Start()
    {
        MilkSlider.value = 0;
        targetValue = 0; // Initialize targetValue to 0

        // Get RectTransforms
        gnomeRectTransform = milkgnome.GetComponent<RectTransform>();
        sliderRectTransform = MilkSlider.GetComponent<RectTransform>();

        Debug.Log("MilkFiller Start: Initialized.");
        Debug.Log($"MilkFiller Instance in Start: {this.GetInstanceID()}");

        // Ensure component is active
        Debug.Log($"MilkFiller is active: {gameObject.activeInHierarchy}");
        Debug.Log($"MilkSlider maxValue: {MilkSlider.maxValue}, MilkSlider value: {MilkSlider.value}");
    }

    private void Update()
    {
        Debug.Log($"Update called: filling={filling}, MilkSlider.value={MilkSlider.value}, targetValue={targetValue}, Instance ID: {this.GetInstanceID()}");

        if (filling && MilkSlider.value < targetValue)
        {
            Debug.Log($"Filling: Current MilkSlider.value = {MilkSlider.value}, Target = {targetValue}");

            float deltaValue = fillSpeed * Time.unscaledDeltaTime;
            MilkSlider.value += deltaValue;
            Debug.Log($"MilkSlider value increased by {deltaValue}, new value = {MilkSlider.value}");

            float normalizedValue = MilkSlider.value / MilkSlider.maxValue;
            Vector3 newPosition = sliderRectTransform.position + new Vector3(0, normalizedValue * sliderRectTransform.rect.height, 0);
            gnomeRectTransform.position = newPosition;

            Debug.Log($"MilkSlider.value updated to = {MilkSlider.value}, MilkGnome position updated to = {gnomeRectTransform.position}");

            if (MilkSlider.value >= 0 && !SoundPlayed1)
            {
                Sound1.Play();
                SoundPlayed1 = true;
                Debug.Log("Sound1 played");
            }

            if (MilkSlider.value >= 33 && !SoundPlayed2)
            {
                Sound2.Play();
                SoundPlayed2 = true;
                Debug.Log("Sound2 played");
            }

            if (MilkSlider.value >= 66 && !SoundPlayed3)
            {
                Sound3.Play();
                SoundPlayed3 = true;
                Debug.Log("Sound3 played");
            }

            if (MilkSlider.value >= 95 && !SoundPlayed4)
            {
                Sound4.Play();
                SoundPlayed4 = true;
                Instantiate(smoePoofUIPrefab, milkgnome.transform.position, Quaternion.identity);
                Debug.Log("Sound4 played and SmoePoofUI instantiated");
            }

            if (MilkSlider.value >= targetValue)
            {
                MilkSlider.value = targetValue;  // Ensure it doesn't overshoot
                filling = false;
                Debug.Log("Filling completed");
            }
        }
    }

    public void SetTargetValue(float value)
    {
        Debug.Log($"SetTargetValue called with value = {value}");
        targetValue = Mathf.Min(value, MilkSlider.maxValue); // Ensure target value does not exceed max value
        filling = true;
        Debug.Log($"SetTargetValue executed: targetValue set to = {targetValue}, filling set to true, Instance ID: {this.GetInstanceID()}");
    }

    public void Initialize(float score)
    {
        Debug.Log($"Initialize called with score = {score}");
        SetTargetValue(score * 16);  // Set target value based on score
        SoundPlayed1 = false;
        SoundPlayed2 = false;
        SoundPlayed3 = false;
        SoundPlayed4 = false;
        filling = true; // Ensure filling is set to true
        Debug.Log($"Initialize executed: resulting in targetValue = {targetValue}, filling set to true, Instance ID: {this.GetInstanceID()}");
    }
}
