using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    private Transform mainCamera;
    public Transform newTarget;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cow" || other.tag == "Dog" || other.tag == "Queen") mainCamera.GetComponent<CameraFollow>().newTarget(newTarget);
    }
}
