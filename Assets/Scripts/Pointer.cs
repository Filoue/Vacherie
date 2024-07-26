using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject visuals;
    public AudioSourceManager audioSourceManager;
    public AudioClip tingleBell;
    public AudioClip poof;
    public AudioClip wind;
    public float distanceThreshold = 10f; // Distance threshold to decide between TingleBell and GnomeSoundPool
    private int tingleBellCount = 0; // Counter for TingleBell sounds played
    private const int maxTingleBellCount = 5; // Max consecutive TingleBell sounds before playing from GnomeSoundPool
    private bool lastSoundWasTingleBell = false; // Flag to track if the last sound was TingleBell to vaoid repeating it

    private EntitiesManager entitiesManager;
    private float nearestDogDistance;
    public float dissapearDistance;

    private void Start()
    {
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        visuals.SetActive(false);
        audioSourceManager = GetComponent<AudioSourceManager>();
    }

    private void Update()
    {
        if (NearestDogDistance(transform.position) < dissapearDistance)
        {
            visuals.SetActive(false);
        }
    }

    public void MovePointerTo(Vector3 position)
    {
        visuals.SetActive(true);
        transform.position = position;

        float nearestDogDist = NearestDogDistance(position);

        if (tingleBellCount < maxTingleBellCount)
        {
            PlayTingleBellSound();
        }
        else
        {
            if (nearestDogDist > distanceThreshold)
            {
                PlayRandomGnomeSound();
                tingleBellCount = 1; // Reset counter after playing from GnomeSoundPool
            }
            else
            {
                PlayTingleBellSound();
            }
        }
    }

    private void PlayTingleBellSound()
    {
        if (audioSourceManager != null)
        {
            if (lastSoundWasTingleBell)
            {
                if (Random.value > 1f / 5f)
                {
                    lastSoundWasTingleBell = false; // Only play Poof sound
                    if (!audioSourceManager.IsPoofSoundPlaying())
                    {
                        audioSourceManager.PlayPoofSound(transform); // Play Poof sound
                    }
                    return;
                }
            }

            audioSourceManager.PlaySound(tingleBell, transform);
            if (!audioSourceManager.IsPoofSoundPlaying())
            {
                audioSourceManager.PlayPoofSound(transform); // Play Poof sound simultaneously if not already playing
            }
            tingleBellCount++;
            lastSoundWasTingleBell = true;
        }
    }

    private void PlayRandomGnomeSound()
    {
        if (audioSourceManager != null)
        {
            audioSourceManager.PlayRandomGnomeSound(transform);
            if (!audioSourceManager.IsWindSoundPlaying())
            {
                audioSourceManager.PlayWindSound(transform); // Play Wind sound simultaneously if not already playing
            }
            lastSoundWasTingleBell = false;
        }
    }

    private float NearestDogDistance(Vector3 point)
    {
        float nearestDistance = Mathf.Infinity;
        foreach (var dog in entitiesManager.dogs)
        {
            float dogDistance = Vector3.Distance(point, dog.transform.position);
            if (dogDistance < nearestDistance)
            {
                nearestDistance = dogDistance;
            }
        }
        return nearestDistance;
    }
}
