
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CowBoids : MonoBehaviour
{
    private Rigidbody rb;

    public CowsManager cowsManager;
    [Range(0.0f, 10.0f)]
    public float speed;
    public float neigbourgRange = 5.0f, avoidRange = 2.0f;
    public float avoidingStrength = 0.1f;

    private float cowDistance;
    private int groupSize = 0;
    private Vector2 vCenter, vAvoid, vSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        groupSize = 0;
        vCenter = Vector2.zero;
        vSpeed = Vector2.zero;
        vAvoid = Vector2.zero;

        foreach (var cow in cowsManager.cows)
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
    }

    private void FixedUpdate()
    {
        Vector2 dirCenter = vCenter - ToVector2(transform.position);
        rb.velocity = new Vector3(dirCenter.x, 0, dirCenter.y) * speed * Time.fixedDeltaTime;
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
