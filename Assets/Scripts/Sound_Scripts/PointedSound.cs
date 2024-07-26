using UnityEngine;
using System.Collections.Generic;

public class PointedSound : MonoBehaviour
{
    public float maxDistance = 10f;
    public float minVolume = 0f;
    public float maxVolume = 1f;
    public float smoothSpeed = 0.5f;

    private Dictionary<AudioSource, float> targetVolumes = new Dictionary<AudioSource, float>();
    private Dictionary<AudioSource, float> currentVolumes = new Dictionary<AudioSource, float>();

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 worldMousePosition = ray.GetPoint(rayDistance);
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

            foreach (AudioSource audioSource in allAudioSources)
            {
                Vector3 soundEmitterPosition = audioSource.transform.position;
                soundEmitterPosition.y = 0;
                float distance = Vector3.Distance(worldMousePosition, soundEmitterPosition);

                float baseVolume = audioSource.GetComponent<BaseVolume>()?.volume ?? 1f;
                float targetVolume = baseVolume * Mathf.Clamp(1 - (distance / maxDistance), minVolume, maxVolume);

                if (!currentVolumes.ContainsKey(audioSource))
                {
                    currentVolumes[audioSource] = audioSource.volume;
                }

                currentVolumes[audioSource] = Mathf.Lerp(currentVolumes[audioSource], targetVolume, smoothSpeed * Time.deltaTime);
                audioSource.volume = currentVolumes[audioSource];
            }
        }
    }
}
