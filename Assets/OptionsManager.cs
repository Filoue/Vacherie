using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
}
