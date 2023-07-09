using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Entity
{
    public void StartChasing()
    {
        base.Chase(chaseTarget);
        isChasing = true;
        isWandering = false;
    }

    public void StopChasing()
    {
        isChasing = false;
        isWandering = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered GameObject is not the detector itself
        if (other.gameObject != gameObject && chaseTarget == null)
        {

            if (other.CompareTag("Player"))
            {
                // The entered GameObject is within the range
                // Debug.Log(other.gameObject.name + " entered the range!");
                chaseTarget = other.gameObject.transform;
                StartChasing();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != gameObject && chaseTarget == null)
        {
            if (other.CompareTag("Prey") || other.CompareTag("Player") || other.CompareTag("InfectedChick"))
            {
                // The entered GameObject is within the range
                //Debug.Log(other.gameObject.name + " entered the range!");
                chaseTarget = other.gameObject.transform;
                StartChasing();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (chaseTarget != null)
        {
            //colliders.Remove(other);


            // Check if the exited GameObject is not the detector itself
            if (other.gameObject == chaseTarget.gameObject)
            {
                // The exited GameObject has left the range
                //Debug.Log(other.gameObject.name + " exited the range!");

                chaseTarget = null;
                StopChasing();
            }
            else
            {
                return;
            }
        }
    }
}
