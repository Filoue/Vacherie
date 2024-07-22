using UnityEngine;

public class SwayBranches : MonoBehaviour
{
    public float swaySpeed = 1.0f;    // Speed of the sway
    public float swayAmount = 10.0f;  // Amount of sway in degrees

    private float initialRotationZ;

    void Start()
    {
        initialRotationZ = transform.localRotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the new rotation angle
        float swayAngle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Apply the new rotation to the z-axis
        transform.localRotation = Quaternion.Euler(0, 0, initialRotationZ + swayAngle);
    }
}
