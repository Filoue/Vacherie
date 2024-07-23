using UnityEngine;

public class PointerSound : MonoBehaviour
{
    public float maxDistance = 10f;
    public float minVolume = 0f;
    public float maxVolume = 1f;

    void Update()
    {
        // Mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Plane at y=0
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            // Get the point on the ground plane where the mouse pointer is
            Vector3 worldMousePosition = ray.GetPoint(rayDistance);

            // Find all AudioSources
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

            foreach (AudioSource audioSource in allAudioSources)
            {
                // Distance between the pointer and the AudioSource's position
                Vector3 soundEmitterPosition = audioSource.transform.position;
                soundEmitterPosition.y = 0; // Ensure y=0 for the AudioSource's position
                float distance = Vector3.Distance(worldMousePosition, soundEmitterPosition);

                // Volume based on the distance
                float volume = Mathf.Clamp(1 - (distance / maxDistance), minVolume, maxVolume);

                // Set the volume
                audioSource.volume = volume;
            }
        }
    }
}
