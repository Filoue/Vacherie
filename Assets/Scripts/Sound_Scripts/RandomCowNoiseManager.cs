using UnityEngine;

public class RandomCowNoiseManager : MonoBehaviour
{
    public static RandomCowNoiseManager Instance { get; private set; }
    private bool isCowNoisePlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanPlayCowNoise()
    {
        return !isCowNoisePlaying;
    }

    public void SetCowNoisePlaying(bool isPlaying)
    {
        isCowNoisePlaying = isPlaying;
    }
}
