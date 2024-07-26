using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public bool animated;

    private void Start()
    {
        offset = transform.position - target.position;
        offset = new Vector3(0, offset.y, offset.z);
    }

    private void Update()
    {
        if (!animated) transform.position = new Vector3(0, target.position.y, target.position.z) + offset;
        else transform.position = target.position + offset;
    }

    public void newTarget(Transform newTarget, bool animate)
    {
        animated = animate;
        target = newTarget;
        offset = transform.position - target.position;
        offset = new Vector3(0, offset.y, offset.z);
    }
}
