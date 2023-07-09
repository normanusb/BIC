using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Entity
{
    private List<Collider> collidersPrey = new List<Collider>();
    public List<Collider> GetCollidersPrey() { return collidersPrey; }

    private List<Collider> collidersPredator = new List<Collider>();
    public List<Collider> GetCollidersPredator() { return collidersPredator; }


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

            /*
            if (other.CompareTag("Prey"))
            {
                if (!collidersPrey.Contains(other)) { collidersPrey.Add(other); }
            }
            else if (other.CompareTag("InfectedChick"))
            {
                if (!collidersPredator.Contains(other)) { collidersPredator.Add(other); }
            }
            else if (other.CompareTag("Player"))
            {

            }

            */

            if (other.CompareTag("Prey") || other.CompareTag("Player") || other.CompareTag("InfectedChick"))
            {
                // The entered GameObject is within the range
                Debug.Log(other.gameObject.name + " entered the range!");
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
                Debug.Log(other.gameObject.name + " exited the range!");

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
