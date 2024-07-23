using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DogAI : MonoBehaviour
{
    private Rigidbody rb;
    public Vector2 target;
    private Vector2 dir;
    public float speed;
    private bool goToTarget;
    public float stopDistance = 0.05f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = ToVector2(transform.position);
        goToTarget = false;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            goToTarget = Vector3.Distance(transform.position, new Vector3(target.x, 0, target.y)) >= stopDistance;

            if (goToTarget)
            {
                dir = (target - ToVector2(transform.position)).normalized * speed;

                float yV = rb.velocity.y;
                rb.velocity = new Vector3(dir.x, 0, dir.y) * Time.fixedDeltaTime;
                rb.velocity = new Vector3(rb.velocity.x, yV, rb.velocity.z);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
