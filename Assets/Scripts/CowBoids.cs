using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CowBoids : MonoBehaviour
{
    private Rigidbody rb;

    private EntitiesManager entitiesManager;
    [Range(0.0f, 10.0f)]
    public float speed;
    public float neigbourgRange = 7.0f, avoidRange = 2.0f, dogAvoidRange = 5.0f;
    public float toCenterMultiplier = 1.0f, avoidMultiplier = 1.0f, followMultiplier = 1.0f, velocityMultiplier = 1.0f, targetMultiplier = 1.0f, avoidDogsMultiplier = 1.0f;
    public Transform target;

    private float cowDistance;
    private int groupSize = 0, toAvoid = 0, dogCount = 0;
    private Vector2 vCenter, vAvoid, vSpeed, vTarget, vDog;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();

        rb.velocity = new Vector3(0, 0, 100);
    }

    private void Update()
    {
        groupSize = 0;
        toAvoid = 0;
        dogCount = 0;
        vCenter = Vector2.zero;
        vSpeed = Vector2.zero;
        vAvoid = Vector2.zero;
        vDog = Vector2.zero;

        foreach (var cow in entitiesManager.cows)
        {
            if (cow != gameObject)
            {
                cowDistance = Vector3.Distance(transform.localPosition, cow.transform.localPosition);

                if (cowDistance <= neigbourgRange)
                {
                    vCenter += ToVector2(cow.transform.localPosition);
                    groupSize++;

                    if (cowDistance < avoidRange)
                    {
                        vAvoid += ToVector2(transform.localPosition) - ToVector2(cow.transform.localPosition);
                        toAvoid++;
                    }

                    Vector2 cowV = ToVector2(cow.GetComponent<Rigidbody>().velocity);
                    vSpeed += cowV;
                }
            }
        }

        if (groupSize > 0)
        {
            vCenter /= groupSize;
            vSpeed /= groupSize;
        }

        if (target != null)
        {
            vTarget = ToVector2(target.position) - ToVector2(transform.position);
        }
        else
        {
            vTarget = Vector2.zero;
        }

        foreach (var dog in entitiesManager.dogs)
        {
            if (Vector3.Distance(transform.position, dog.transform.position) < dogAvoidRange)
            {
                vDog += ToVector2(transform.position) - ToVector2(dog.transform.position);
                dogCount++;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 dirCenter = (vCenter - ToVector2(transform.position)).normalized * toCenterMultiplier;
        Vector2 dirAvoid = vAvoid.normalized * toAvoid * avoidMultiplier;
        Vector2 dirAvoidDog = vDog.normalized * dogCount * avoidDogsMultiplier;
        Vector2 dirFollow = vSpeed.normalized * followMultiplier;
        Vector2 dirVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * velocityMultiplier;
        Vector2 dirTarget = vTarget.normalized * targetMultiplier;

        Vector2 finalV = dirCenter + dirAvoid + dirFollow + dirTarget + dirVelocity + dirAvoidDog;

        Mathf.Clamp(finalV.magnitude, 0, speed);

        float yV = rb.velocity.y;

        rb.velocity = new Vector3(finalV.x, 0, finalV.y) * Time.fixedDeltaTime;
        rb.velocity = new Vector3(rb.velocity.x, yV, rb.velocity.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(vCenter.x, 0, vCenter.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(vAvoid.x, 0, vAvoid.y));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(vSpeed.x, 0, vSpeed.y));

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
