using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceManager : MonoBehaviour
{
    public SoundPool dogSoundPool;
    public SoundPool gnomeSoundPool;
    public SoundPool cowBellSoundPool;
    public SoundPool cowNoiseSoundPool;
    public AudioClip poof;
    public AudioClip wind;

    private List<AudioClip> remainingDogClips;
    private List<AudioClip> remainingGnomeClips;
    private List<AudioClip> remainingCowBellClips;
    private List<AudioClip> remainingCowNoiseClips;
    private bool isPoofSoundPlaying = false;
    private bool isWindSoundPlaying = false;

    private void Start()
    {
        ResetDogSoundPool();
        ResetGnomeSoundPool();
        ResetCowBellSoundPool();
        ResetCowNoiseSoundPool();
    }

    public void PlayDogSound(Transform parent)
    {
        if (remainingDogClips == null || remainingDogClips.Count == 0)
        {
            ResetDogSoundPool();
        }

        int randomIndex = Random.Range(0, remainingDogClips.Count);
        AudioClip clip = remainingDogClips[randomIndex];
        remainingDogClips.RemoveAt(randomIndex);

        AudioSource source = parent.gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0.5f;
        source.Play();
        Destroy(source, clip.length);
    }

    public void PlaySound(AudioClip clip, Transform parent)
    {
        AudioSource source = parent.gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        Destroy(source, clip.length);
    }

    public void PlaySoundWithVolume(AudioClip clip, Transform parent, float volume)
    {
        AudioSource source = parent.gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        var baseVolume = source.gameObject.AddComponent<BaseVolume>();
        baseVolume.volume = volume;
        source.Play();
        Destroy(source, clip.length);
    }

    public void PlayPoofSound(Transform parent)
    {
        if (!isPoofSoundPlaying)
        {
            isPoofSoundPlaying = true;
            AudioSource source = parent.gameObject.AddComponent<AudioSource>();
            source.clip = poof;
            source.Play();
            Destroy(source, poof.length);
            StartCoroutine(ResetPoofSoundFlag(poof.length));
        }
    }

    public void PlayWindSound(Transform parent)
    {
        if (!isWindSoundPlaying)
        {
            isWindSoundPlaying = true;
            AudioSource source = parent.gameObject.AddComponent<AudioSource>();
            source.clip = wind;
            source.Play();
            Destroy(source, wind.length);
            StartCoroutine(ResetWindSoundFlag(wind.length));
        }
    }

    public void PlayRandomGnomeSound(Transform parent)
    {
        if (remainingGnomeClips == null || remainingGnomeClips.Count == 0)
        {
            ResetGnomeSoundPool();
        }

        int randomIndex = Random.Range(0, remainingGnomeClips.Count);
        AudioClip currentGnomeClip = remainingGnomeClips[randomIndex];
        remainingGnomeClips.RemoveAt(randomIndex);

        AudioSource source = parent.gameObject.AddComponent<AudioSource>();
        source.clip = currentGnomeClip;
        source.Play();
        Destroy(source, currentGnomeClip.length);
    }

    public void PlayRandomCowBellSound(Transform parent, float volume)
    {
        if (remainingCowBellClips == null || remainingCowBellClips.Count == 0)
        {
            ResetCowBellSoundPool();
        }

        int randomIndex = Random.Range(0, remainingCowBellClips.Count);
        AudioClip currentCowBellClip = remainingCowBellClips[randomIndex];
        remainingCowBellClips.RemoveAt(randomIndex);

        AudioSource source = parent.gameObject.AddComponent<AudioSource>();
        source.clip = currentCowBellClip;
        source.volume = volume;
        var baseVolume = source.gameObject.AddComponent<BaseVolume>();
        baseVolume.volume = volume;
        source.Play();
        Destroy(source, currentCowBellClip.length);
    }

    public void PlayRandomCowNoiseSound(Transform parent, float volume)
    {
        if (remainingCowNoiseClips == null || remainingCowNoiseClips.Count == 0)
        {
            ResetCowNoiseSoundPool();
        }

        int randomIndex = Random.Range(0, remainingCowNoiseClips.Count);
        AudioClip currentCowNoiseClip = remainingCowNoiseClips[randomIndex];
        remainingCowNoiseClips.RemoveAt(randomIndex);

        AudioSource source = parent.gameObject.AddComponent<AudioSource>();
        source.clip = currentCowNoiseClip;
        source.volume = volume;
        var baseVolume = source.gameObject.AddComponent<BaseVolume>();
        baseVolume.volume = volume;
        source.Play();
        Destroy(source, currentCowNoiseClip.length);
        Debug.Log("Playing cow noise: " + currentCowNoiseClip.name);
    }

    private IEnumerator ResetPoofSoundFlag(float duration)
    {
        yield return new WaitForSeconds(duration);
        isPoofSoundPlaying = false;
    }

    private IEnumerator ResetWindSoundFlag(float duration)
    {
        yield return new WaitForSeconds(duration);
        isWindSoundPlaying = false;
    }

    public bool IsPoofSoundPlaying()
    {
        return isPoofSoundPlaying;
    }

    public bool IsWindSoundPlaying()
    {
        return isWindSoundPlaying;
    }

    private void ResetDogSoundPool()
    {
        remainingDogClips = new List<AudioClip>(dogSoundPool.clips);
    }

    private void ResetGnomeSoundPool()
    {
        remainingGnomeClips = new List<AudioClip>(gnomeSoundPool.clips);
    }

    private void ResetCowBellSoundPool()
    {
        remainingCowBellClips = new List<AudioClip>(cowBellSoundPool.clips);
    }

    private void ResetCowNoiseSoundPool()
    {
        remainingCowNoiseClips = new List<AudioClip>(cowNoiseSoundPool.clips);
    }
}
