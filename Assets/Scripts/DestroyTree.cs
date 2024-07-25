using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTree : MonoBehaviour
{
    private bool isIn;


    private void Update()
    {
        if (isIn)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            isIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Road") && isIn == true)
        {
            isIn = false;
        }
    }
}
