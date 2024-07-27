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

    private void Start()
    {
        staminaSlider.value = 0;
    }

    private void Update()
    {
        if (staminaSlider.value < staminaSlider.maxValue)
        {
            staminaSlider.value += fillSpeed * Time.deltaTime;

            if (!startSoundPlayed && staminaSlider.value > 0)
            {
                startFillSound.Play();
                startSoundPlayed = true;
            }

            if (!oneThirdSoundPlayed && staminaSlider.value >= 33)
            {
                oneThirdSound.Play();
                oneThirdSoundPlayed = true;
            }

            if (!twoThirdsSoundPlayed && staminaSlider.value >= 66)
            {
                twoThirdsSound.Play();
                twoThirdsSoundPlayed = true;
            }

            if (!fullSoundPlayed && staminaSlider.value >= 95)
            {
                fullSound.Play();
                fullSoundPlayed = true;
                Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0);
                GameObject milkgnome = Instantiate(milkgnomePrefab, spawnPosition, Quaternion.identity);
                milkgnome.transform.localScale = new Vector3(3, 3, 3);
            }
        }
    }
}
