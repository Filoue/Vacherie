using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
        offset = new Vector3(0, offset.y, offset.z);
    }

    private void Update()
    {
        transform.position = new Vector3(0, target.position.y, target.position.z) + offset;
    }

    public void newTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
        offset = new Vector3(0, offset.y, offset.z);
    }
}
