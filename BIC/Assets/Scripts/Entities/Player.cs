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

    public float shootForce;
    public float upwardForce;

    public GameObject shootPosition;
    public GameObject playerVisuals;
    public GameObject projectileChick;
    public GameObject projectileChickInfected;

    private GameObject projectileToThrow;

    public Transform closestEntity = null;

    public float throwableChicksRadius = 10f;
    public List<Transform> followingEntities = new List<Transform>();

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        //PLAY STEPS SOUNDS WITH FMOD
    }

    //Throwing Normal Chick
    public void OnThrowChick(InputAction.CallbackContext context)
    {
        //Shoot when you release the button
        if (context.canceled)
        {
            if (closestEntity != null)
            {
                Throw();
            }
        }

    }


    //Changing Projectile to InfectedChick if you hold click
    public void OnThrowInfectedChick(InputAction.CallbackContext context)
    {
        //When you hold the button
        if (context.performed)
        {
            if (closestEntity != null)
            {
                InfectChickInHand();
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        dead = false;

        //Set normal projectile first
        projectileToThrow = projectileChick;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        if (dead)
        {
            StartCoroutine(playersDeath());
        }

        CheckFollowingEntities();
        FindClosestEntity();
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
        if (health <= 0)
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

    public void Throw()
    {
        if (closestEntity != null && projectileToThrow == projectileChick)
        {
            Destroy(closestEntity.gameObject);
            ProjectileLogic();
        }
        else if (projectileToThrow == projectileChickInfected)
        {
            ProjectileLogic();
        }

    }

    public void ProjectileLogic()
    {
        //Shoot Projectile
        GameObject projectile = Instantiate(projectileToThrow, shootPosition.transform.position, shootPosition.transform.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // Add a forward force to the projectile
        projectileRigidbody.AddForce(transform.forward * shootForce, ForceMode.VelocityChange);

        // Add an upward force to the projectile
        projectileRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.VelocityChange);

        //Switch back to Normal Projectile
        projectileToThrow = projectileChick;
    }

    public void InfectChickInHand()
    {
        Debug.Log("Infected Chick ready to throw!");

        //Delete One Chick
        Destroy(closestEntity.gameObject);

        //Spawn Chick next to Player so it seems like its charging it up before throwing


        //change projectile to infected 
        projectileToThrow = projectileChickInfected;
    }
    private void CheckFollowingEntities()
    {
        // Clear the list of following entities
        followingEntities.Clear();

        // Find all entities within the detection radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, throwableChicksRadius);

        foreach (Collider collider in colliders)
        {
            // Check if the entity is following the player
            if (collider.CompareTag("Prey"))
            {
                followingEntities.Add(collider.transform);
            }
        }
    }

    private void FindClosestEntity()
    {
        closestEntity = null;
        float closestDistance = Mathf.Infinity;

        // Iterate through the list of following entities to find the closest one
        foreach (Transform entity in followingEntities)
        {
            float distance = Vector3.Distance(transform.position, entity.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEntity = entity;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, throwableChicksRadius);
    }
}
