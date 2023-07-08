using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChick : MonoBehaviour
{

    public GameObject spawnee;

    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate the spawnPrefab at the same position and rotation as this GameObject
        Instantiate(spawnee, transform.position, transform.rotation);

        // Destroy this GameObject
        Destroy(gameObject);
    }

}
