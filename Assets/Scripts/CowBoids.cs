using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class CowBoids : MonoBehaviour
{
    private Vector2 v1, v2, v3;
    // v1 - First rule: Boids try to fly towards the centre of mass of neighbouring boids.
    // v2 - Second rule: Boids try to keep a small distance away from othe objects (including other boids).
    // v3 - Third rule: Boids try to match velocity with near boids.

    private Vector2 pc;
    // c - The percieved center of mass of all the boids (not including itself)

    private Rigidbody rb;

    public CowsManager cowsManager;
    public float ruleOneMultiplier = 1.0f, avoidanceRange = 5.0f, ruleThreeMultiplier = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        v1 = RuleOne();
        v2 = RuleTwo();
    }

    private Vector2 RuleOne()
    {
        // Calculate the percieved center of mass of the herd
        pc = Vector2.zero;
        List<GameObject> cowsWithoutSelf = cowsManager.cows;
        cowsWithoutSelf.Remove(gameObject);

        foreach (var cow in cowsWithoutSelf)
        {
            pc += new Vector2(cow.transform.position.x, cow.transform.position.z);
        }

        pc /= cowsWithoutSelf.Count;

        return (pc - new Vector2(transform.position.x, transform.position.z)) / 100 * ruleOneMultiplier;
    }

    private Vector2 RuleTwo()
    {
        return Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.velocity += (new Vector3(v1.x, 0, v1.y) + new Vector3(v2.x, 0, v2.y) + new Vector3(v3.x, 0, v3.y)) / Time.fixedDeltaTime;
    }
}
