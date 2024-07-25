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
    private EntitiesManager entitiesManager;
    private float cowDistance;
    public float neigbourgRange = 7.0f;
    private Vector2 vSpeed;
    private int groupSize = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = ToVector2(transform.position);
        goToTarget = false;
        entitiesManager = GameObject.FindGameObjectWithTag("EntitiesManager").GetComponent<EntitiesManager>();
    }

    private void Update()
    {
        vSpeed = Vector2.zero;
        groupSize = 0;

        foreach (var cow in entitiesManager.cows)
        {
            if (cow != gameObject)
            {
                cowDistance = Vector3.Distance(transform.localPosition, cow.transform.localPosition);

                if (cowDistance <= neigbourgRange)
                {
                    Vector2 cowV = ToVector2(cow.GetComponent<Rigidbody>().velocity);
                    vSpeed += cowV;
                    groupSize++;
                }
            }
        }

        if (groupSize > 0)
        {
            vSpeed /= groupSize;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10.0f) Die();

        if (Vector3.Distance(transform.position, new Vector3(target.x, 0, target.y)) < stopDistance)
        {
            goToTarget = false;
        }

        if (goToTarget)
        {
            dir = (target - ToVector2(transform.position)).normalized * speed;

            float yV = rb.velocity.y;
            rb.velocity = new Vector3(dir.x, 0, dir.y) * Time.fixedDeltaTime;
            rb.velocity = new Vector3(rb.velocity.x, yV, rb.velocity.z);
        }
        else
        {
            dir = vSpeed;

            float yV = rb.velocity.y;
            rb.velocity = new Vector3(dir.x, 0, dir.y);
            rb.velocity = new Vector3(rb.velocity.x, yV, rb.velocity.z);
        }
    }

    public void GoToTarget(Vector3 target)
    {
        this.target = new Vector2(target.x, target.z);
        goToTarget = true;
    }

    public void Die()
    {
        entitiesManager.dogs.Remove(gameObject);
        Destroy(gameObject);
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
