using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{
    private Transform camera;
    public Transform newTarget;

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cow" || other.tag == "Dog" || other.tag == "Queen") camera.GetComponent<CameraFollow>().newTarget(newTarget);
    }
}
