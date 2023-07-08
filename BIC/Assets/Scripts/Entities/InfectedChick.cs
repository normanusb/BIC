using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedChick : Predator
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered GameObject is not the detector itself
        if (other.gameObject != gameObject && chaseTarget == null)
        {
            if (other.CompareTag("Predator") || other.CompareTag("Player") || other.CompareTag("Prey"))
            {
                // The entered GameObject is within the range
                Debug.Log(other.gameObject.name + " entered the range of Infected Chick!");
                chaseTarget = other.gameObject.transform;
                StartChasing();
            }

        }
    }
}
