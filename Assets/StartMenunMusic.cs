using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenunMusic : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindWithTag("Travel").GetComponent<SoundManager>();
        soundManager.PlayMenuMusic();
    }
}
