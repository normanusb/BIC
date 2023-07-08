using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Entity
{
    public float perceptionRange;
    // Start is called before the first frame update

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, perceptionRange);
    }
}
