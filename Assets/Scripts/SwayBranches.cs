using UnityEngine;

public class SwayBranches : MonoBehaviour
{
    public float swaySpeed = 1.0f;         
    public float swayAmount = 5.0f; 
    public float randomness = 0.2f;  
    public float bulgeSpeed = 0.5f;        
    public float bulgeAmount = 0.1f;        

    private float initialRotationZ;
    private Vector3 initialScale;
    private float randomOffset;

    void Start()
    {
        initialRotationZ = transform.localRotation.eulerAngles.z;
        initialScale = transform.localScale;
        randomOffset = Random.Range(-randomness, randomness);
    }

    void Update()
    {

        float swayAngle = Mathf.Sin(Time.time * swaySpeed + randomOffset) * swayAmount;


        float bulgeFactor = 1 + Mathf.Sin(Time.time * bulgeSpeed + randomOffset) * bulgeAmount;


        transform.localRotation = Quaternion.Euler(0, 0, initialRotationZ + swayAngle);

        transform.localScale = initialScale * bulgeFactor;
    }
}
