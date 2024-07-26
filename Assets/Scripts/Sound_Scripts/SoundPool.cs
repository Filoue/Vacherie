using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundPool", menuName = "Audio/Sound Pool")]
public class SoundPool : ScriptableObject
{
    public AudioClip[] clips;
}
