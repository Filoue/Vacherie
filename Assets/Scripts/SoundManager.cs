using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;

    public AudioClip gameMusic;
    public GameObject ambientSounds;
    private AudioSource[] ambientSources;

    private void Start()
    {
        ambientSources = ambientSounds.GetComponents<AudioSource>();
    }
}
