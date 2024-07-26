using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    public List<DogAI> dogs;
    public float unlockDistance = 30f;

    void Start()
    {
        dogs = new List<DogAI>(FindObjectsOfType<DogAI>());
    }

    void Update()
    {
        Vector3 cursorPosition = GetCursorWorldPosition();

        foreach (DogAI dog in dogs)
        {
            if (dog.IsLocked())
            {
                dog.UnlockIfTooFar(cursorPosition);
            }
        }

        if (AllDogsLocked())
        {
            UnlockNearestDogToCursor(cursorPosition);
        }
    }

    private Vector3 GetCursorWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Plane at y=0
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        return Vector3.zero;
    }

    private bool AllDogsLocked()
    {
        foreach (DogAI dog in dogs)
        {
            if (!dog.IsLocked())
            {
                return false;
            }
        }
        return true;
    }

    private void UnlockNearestDogToCursor(Vector3 cursorPosition)
    {
        DogAI nearestDog = null;
        float nearestDistance = Mathf.Infinity;

        foreach (DogAI dog in dogs)
        {
            float distance = Vector3.Distance(dog.transform.position, cursorPosition);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestDog = dog;
            }
        }

        if (nearestDog != null)
        {
            nearestDog.Unlock();
        }
    }
}
