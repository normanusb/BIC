using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    public int health;
    public static bool dead;
    
    public GameObject playerVisuals;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        //PLAY STEPS SOUNDS WITH FMOD
    }
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        if (dead) 
        {
            StartCoroutine(playersDeath());
        }
    }

    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void TakeDamage(int damageAmount) 
    {
        health -= damageAmount;
        if (health <=0) 
        {
            dead = true;
        }
    }
    public IEnumerator playersDeath() 
    {
        //Deactivate Visuals
        playerVisuals.SetActive(false);
        //Deactivate Controlers
        GetComponent<PlayerInput>().enabled = false;
        //Deactivate triggers and colliders
        GetComponent<BoxCollider>().enabled = false;

        //Freeze Rigidbody
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        //Deactivate Player Audio?????
        //playerAudio.SetActive(false);

        //Wait seconds
        yield return new WaitForSeconds(2);

        //Reload Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
