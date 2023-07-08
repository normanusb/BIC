using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectEggs : MonoBehaviour
{
  public GameObject chickprefab;
  private void OnTriggerEnter(Collider other) 
  {
    Player player = other.GetComponent<Player>();

    if (player != null) 
    {
        Instantiate(chickprefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
  }
}
