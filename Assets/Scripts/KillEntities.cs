using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEntities : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cow" || other.tag == "Queen") other.GetComponent<CowBoids>().Die();
        if (other.tag == "Dog") other.GetComponent<DogAI>().Die();
    }
}
