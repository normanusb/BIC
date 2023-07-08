using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class babyfox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            RemoveAllEnemies();
            StartCoroutine(playerWin(other));
        }
    }

    public void RemoveAllEnemies()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Predator"))
        {
            Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("InfectedChick"))
        {
            Destroy(go);
        }
    }

    public IEnumerator playerWin(Collider other)
    {
        //Deactivate Controlers
        other.GetComponent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        // TODO: Make this script go to NeW Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
