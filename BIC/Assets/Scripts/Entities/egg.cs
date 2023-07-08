using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerCollectEggs : MonoBehaviour
{
  public GameObject chickprefab;

    //VFX
    [SerializeField] private EventReference eggCollectedSound;
    [SerializeField] private GameObject eggCollectingobject;

  private void OnTriggerEnter(Collider other) 
  {
    Player player = other.GetComponent<Player>();

    if (player != null) 
    {
        //SFX
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PLAYER/Player_CollectChick", eggCollectingobject);
            Instantiate(chickprefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
  }
}
