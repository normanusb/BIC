using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerCollectEggs : MonoBehaviour
{
  public GameObject chickprefab;

    //VFX
    [SerializeField] private EventReference eggCollectedSound;

  private void OnTriggerEnter(Collider other) 
  {
    Player player = other.GetComponent<Player>();

    if (player != null) 
    {
        //SFX
        AudioManager.instance.PlayOneShot(eggCollectedSound);
        Instantiate(chickprefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
  }
}
