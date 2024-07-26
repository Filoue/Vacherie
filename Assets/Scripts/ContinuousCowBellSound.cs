using UnityEngine;
using System.Collections;

public class ContinuousCowBellSound : MonoBehaviour
{
    public AudioSourceManager audioSourceManager;
    public float cowBellVolume = 0.5f;

    private void Start()
    {
        if (audioSourceManager == null)
        {
            audioSourceManager = FindObjectOfType<AudioSourceManager>();
        }
        StartCoroutine(PlayCowBellSound());
    }

    private IEnumerator PlayCowBellSound()
    {
        while (true)
        {
            audioSourceManager.PlayRandomCowBellSound(transform, cowBellVolume);
            yield return new WaitForSeconds(Random.Range(5f, 10f)); // Adjust the time range as needed
        }
    }
}
