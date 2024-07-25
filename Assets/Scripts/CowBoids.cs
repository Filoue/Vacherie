using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CowBoids : MonoBehaviour
{
    private Rigidbody rb;

    private EntitiesManager entitiesManager;
    [Range(0.0f, 200.0f)]
    public float speed;
    public float neigbourgRange = 7.0f, avoidRange = 2.0f, dogAvoidRange = 5.0f, queenAvoidRange = 2.0f, queenVisibilityRange = 10.0f;
    public float toCenterMultiplier = 1.0f, avoidMultiplier = 1.0f, followMultiplier = 1.0f, velocityMultiplier = 1.0f, targetMultiplier = 1.0f, avoidDogsMultiplier = 1.0f, avoidQueenMultiplier = 1.0f;
    private Vector2 dirAvoid, dirAvoidDog, dirFollow, dirVelocity, dirTarget, dirAvoidQueen, finalV;
    public Transform target;
    private Vector3 targetPosition;
    public bool followQueen;

    private Vector3 selfPosition;

    private float cowDistance;
    private int groupSize = 0, toAvoid = 0, dogCount = 0;
    private Vector2 vCenter, vAvoid, vSpeed, vTarget, vDog, vAvoidQueen;
    private Transform queen;
    private GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        entitiesManager = GameObject.FindWithTag("EntitiesManager").GetComponent<EntitiesManager>();

        queen = GameObject.FindGameObjectWithTag("Queen").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        selfPosition = transform.position;
        if (target != null) targetPosition = target.position;
        groupSize = 0;
        toAvoid = 0;
        dogCount = 0;
        vCenter = Vector2.zero;
        vSpeed = Vector2.zero;
        vAvoid = Vector2.zero;
        vDog = Vector2.zero;
        vAvoidQueen = Vector2.zero;

        if (queen == null)
        {
            gameManager.Lose();
        }

        foreach (var cow in entitiesManager.cows)
        {
            if (cow != gameObject)
            {
                Vector3 cowPosition3 = cow.transform.position;
                cowDistance = Vector3.Distance(selfPosition, cowPosition3);

                if (cowDistance <= neigbourgRange)
                {
                    Vector2 cowPosition2 = ToVector2(cowPosition3);
                    vCenter += cowPosition2;
                    groupSize++;

                    if (cowDistance < avoidRange)
                    {
                        vAvoid += ToVector2(selfPosition) - cowPosition2;
                        toAvoid++;
                    }

                    vSpeed += ToVector2(cow.GetComponent<Rigidbody>().velocity);
                }
            }
        }

        if (groupSize > 0)
        {
            vCenter /= groupSize;
            vSpeed /= groupSize;
        }

        if (queen != null && Vector3.Distance(queen.position, selfPosition) < queenVisibilityRange && followQueen) target = queen;

        if (target != null)
        {
            Vector2 temp = ToVector2(targetPosition) - ToVector2(selfPosition);
            if (!followQueen)
            {
                vTarget = temp;
            }
            else
            {
                if (Vector3.Distance(targetPosition, selfPosition) < queenVisibilityRange)
                {
                    vTarget = temp;
                }
                else
                {
                    vTarget = Vector2.zero;
                }
            }
        }
        else
        {
            vTarget = Vector2.zero;
        }

        foreach (var dog in entitiesManager.dogs)
        {

            if (Vector3.Distance(selfPosition, dog.transform.position) < dogAvoidRange)
            {
                vDog += ToVector2(selfPosition) - ToVector2(dog.transform.position);
                dogCount++;
            }
        }

        if (queen != null && entitiesManager.queen != gameObject)
        {
            if (Vector3.Distance(selfPosition, queen.position) < queenAvoidRange)
            {
                vAvoidQueen = ToVector2(selfPosition) - ToVector2(queen.position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (selfPosition.y < -10.0f) Die();

        Vector2 dirCenter = Vector2.zero;

        if (groupSize > 0)
        {
            dirCenter = (vCenter - ToVector2(selfPosition)).normalized * toCenterMultiplier;
        }
        dirAvoid = vAvoid.normalized * toAvoid * avoidMultiplier;
        dirAvoidDog = vDog.normalized * dogCount * avoidDogsMultiplier;
        dirFollow = vSpeed.normalized * followMultiplier;
        dirVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * velocityMultiplier;
        dirTarget = vTarget.normalized * targetMultiplier;
        dirAvoidQueen = vAvoidQueen.normalized * avoidQueenMultiplier;

        finalV = dirCenter + dirAvoid + dirFollow + dirTarget + dirVelocity + dirAvoidDog + dirAvoidQueen;

        print(finalV.magnitude);
        if (finalV.magnitude > speed) finalV = finalV.normalized * speed;
        print(finalV.magnitude);

        float yV = rb.velocity.y;

        rb.velocity = new Vector3(finalV.x, 0, finalV.y) * Time.fixedDeltaTime;
        rb.velocity = new Vector3(rb.velocity.x, yV, rb.velocity.z);
    }

    public void Die()
    {
        entitiesManager.cows.Remove(gameObject);
        Destroy(gameObject);
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
