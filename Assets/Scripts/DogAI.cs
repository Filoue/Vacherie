using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider2D))]
public class DogAI : MonoBehaviour
{
    private Rigidbody rb;
    public Vector2 target;
    private Vector2 dir;
    public float speed;
    private float currentSpeed;
    private bool goToTarget;
    private bool isLocked;
    private AudioSource sitAudioSource;
    public float stopDistance = 0.05f;
    private EntitiesManager entitiesManager;
    private float cowDistance;
    public float neigbourgRange = 7.0f;
    private Vector2 vSpeed;
    private int groupSize = 0;
    public AudioSourceManager audioSourceManager;
    public float moveDelay = 2.0f; // Set delay time before running full speed
    public AudioClip dogSitSound;
    public float unlockDistance = 30f; // Distance to unlock the dog if it's too far from cursor

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = ToVector2(transform.position);
        goToTarget = false;
        isLocked = false;
        entitiesManager = GameObject.FindGameObjectWithTag("EntitiesManager").GetComponent<EntitiesManager>();
        audioSourceManager = GetComponent<AudioSourceManager>();
        sitAudioSource = gameObject.AddComponent<AudioSource>();
        sitAudioSource.clip = dogSitSound;
        sitAudioSource.loop = true;
    }

    private void Update()
    {
        if (isLocked)
        {
            sitAudioSource.volume = Mathf.Lerp(sitAudioSource.volume, 1.0f, Time.deltaTime * 2f);
            return; // Skip other updates if locked
        }

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

        if (isLocked) return;

        if (Vector3.Distance(transform.position, new Vector3(target.x, 0, target.y)) < stopDistance)
        {
            goToTarget = false;
        }

        if (goToTarget)
        {
            dir = (target - ToVector2(transform.position)).normalized * currentSpeed;

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
        if (isLocked) return;

        this.target = new Vector2(target.x, target.z);
        goToTarget = true; // Start moving immediately
        currentSpeed = speed / 3f; // Set speed to 1/3
        StopAllCoroutines();
        StartCoroutine(DelayedMove());
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(moveDelay - 0.2f); // Wait for the move delay minus 0.2 seconds
        PlayDogSound(); // Play the dog sound 0.2 seconds before the end of the delay
        yield return new WaitForSeconds(0.2f); // Wait for the remaining 0.2 seconds
        currentSpeed = speed; // Set to full speed after delay
    }

    private void PlayDogSound()
    {
        if (audioSourceManager != null)
        {
            audioSourceManager.PlayDogSound(transform); // Pass the transform to the audio source manager
        }
    }

    private void Die()
    {
        entitiesManager.dogs.Remove(gameObject);
        Destroy(gameObject);
    }

    private Vector2 ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }

    private void OnMouseDown()
    {
        if (isLocked)
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    public void Lock()
    {
        isLocked = true;
        rb.velocity = Vector3.zero; // Stop movement
        sitAudioSource.Play();
    }

    public void Unlock()
    {
        isLocked = false;
        sitAudioSource.Stop();
        PlayDogSound();
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void UnlockIfTooFar(Vector3 cursorPosition)
    {
        if (Vector3.Distance(transform.position, cursorPosition) > unlockDistance)
        {
            Unlock();
        }
    }
}
