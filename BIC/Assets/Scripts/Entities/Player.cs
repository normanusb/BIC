using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    public static int health = 2;
    public static bool isGameOver;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        //PLAY STEPS SOUNDS WITH FMOD
    }
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        if (isGameOver) 
        {
            playersDeath();
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
            isGameOver = true;
        }
    }
    public IEnumerator playersDeath() 
    {

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
