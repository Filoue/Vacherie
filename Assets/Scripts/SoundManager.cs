using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioSource audioSource;
    public AudioClip gameMusic;
    public AudioClip menuMusic;
    public AudioClip endMusic;

    private bool ambient;
    private float tempAmbient;

    private bool entities;
    private float tempEntities;

    private void Start()
    {
        ambient = true;
    }


    public void ToggleAmbient()
    {
        if (ambient)
        {
            ambient = false;
            audioMixer.GetFloat("Ambient", out tempAmbient);
            audioMixer.SetFloat("Ambient", 0);
        }
        else
        {
            ambient = true;
            audioMixer.SetFloat("Ambient", tempAmbient);
        }
    }

    public void ToggleEntities()
    {
        if (entities)
        {
            entities = false;
            audioMixer.GetFloat("Entities", out tempEntities);
            audioMixer.SetFloat("Entities", 0);
        }
        else
        {
            entities = true;
            audioMixer.SetFloat("Entities", tempEntities);
        }
    }

    public void PlayEndMusic()
    {
        audioSource.clip = endMusic;
    }

    public void PlayGameMusic()
    {
        audioSource.clip = gameMusic;
    }
}
