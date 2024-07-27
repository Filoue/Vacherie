using UnityEngine;
using UnityEngine.UI;

public class MilkFill : MonoBehaviour
{
    public Slider staminaSlider;
    public float fillSpeed = 20f;
    public AudioSource startFillSound;
    public AudioSource oneThirdSound;
    public AudioSource twoThirdsSound;
    public AudioSource fullSound;
    public GameObject milkgnomePrefab;
    private bool startSoundPlayed = false;
    private bool oneThirdSoundPlayed = false;
    private bool twoThirdsSoundPlayed = false;
    private bool fullSoundPlayed = false;

    private float targetValue = 0; // The target value to fill up to

    private void Start()
    {
        staminaSlider.value = 0;
    }

    private void Update()
    {
        if (staminaSlider.value < targetValue)
        {
            staminaSlider.value += fillSpeed * Time.deltaTime;

            if (!startSoundPlayed && staminaSlider.value > 0)
            {
                startFillSound.Play();
                startSoundPlayed = true;
            }

            if (!oneThirdSoundPlayed && staminaSlider.value >= targetValue / 3)
            {
                oneThirdSound.Play();
                oneThirdSoundPlayed = true;
            }

            if (!twoThirdsSoundPlayed && staminaSlider.value >= (2 * targetValue) / 3)
            {
                twoThirdsSound.Play();
                twoThirdsSoundPlayed = true;
            }

            if (!fullSoundPlayed && staminaSlider.value >= targetValue)
            {
                fullSound.Play();
                fullSoundPlayed = true;
                Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0);
                GameObject milkgnome = Instantiate(milkgnomePrefab, spawnPosition, Quaternion.identity);
                milkgnome.transform.localScale = new Vector3(3, 3, 3);
            }
        }
    }

    public void SetTargetValue(float value)
    {
        targetValue = value;
    }
}
