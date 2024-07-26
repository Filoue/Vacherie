using UnityEngine;
using System.Collections;

public class RandomCowNoise : MonoBehaviour
{
    public AudioSourceManager audioSourceManager;
    public float cowNoiseVolume = 0.5f;
    public float baseChance = 0.1f; // Base chance of playing a noise
    private EntitiesManager entitiesManager;

    private void Start()
    {
        if (audioSourceManager == null)
        {
            audioSourceManager = FindObjectOfType<AudioSourceManager>();
        }
        entitiesManager = GameObject.FindGameObjectWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        StartCoroutine(PlayCowNoise());
    }

    private IEnumerator PlayCowNoise()
    {
        while (true)
        {
            int cowCount = entitiesManager.cows.Count;
            float playChance = baseChance + cowCount * 0.01f; // Increase chance based on the number of cows



            if (Random.value < playChance && RandomCowNoiseManager.Instance.CanPlayCowNoise())
            {

                RandomCowNoiseManager.Instance.SetCowNoisePlaying(true);
                audioSourceManager.PlayRandomCowNoiseSound(transform, cowNoiseVolume);
                yield return new WaitForSeconds(Random.Range(5f, 10f)); // Adjust the time range as needed
                RandomCowNoiseManager.Instance.SetCowNoisePlaying(false);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1f, 2f)); // Shorter wait time if no sound was played
            }
        }
    }
}
