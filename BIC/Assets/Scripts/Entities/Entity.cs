using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("*Adjustable Stats*")]
    public bool prey;
    public bool predator;
    public int maxHealth;
    public float speed;
    public float chaseSpeed;
    public int attackDamage;
    public int rotationSpeed;
    public float targetReachedDistance;

    [Header("*Variables*")]
    public int health;
    private Vector3 targetDirection;
    private float speedBox;
    private float chaseSpeedBox;
    private Transform chaseTarget;
    public float distanceToTarget;
    private bool moving = true;

    [Header("*States*")]
    public bool isWandering;
    public bool isChasing;
    public bool isFleeing;
    public bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        speedBox = speed;
        chaseSpeedBox = chaseSpeed;
        isWandering = false;
        isChasing = true;

        chaseTarget = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        WanderAround();
        Chase(chaseTarget);
        CalculateDistanceToTarget(chaseTarget);

    }

    public void WanderAround()
    {
        if (isWandering)
        {
            // Move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Rotate randomly
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            // Chance for the Entity to change direction
            if (Random.Range(0, 100) < 1)
            {
                ChangeDirection();
            }

            // Chance for the Entity to Stop walking
            if (Random.Range(0, 1000) < 1)
            {
                StartCoroutine(Stop());
            }
        }
    }

    private void ChangeDirection()
    {
        // Generate a new random direction
        float angle = Random.Range(0, 360);
        targetDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
    }
    private IEnumerator Stop()
    {
        if (moving)
        {
            moving = false;
            speed = 0;
            chaseSpeed = 0;
            yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));
            StartWalking();
        }
    }

    private void StartWalking()
    {
        speed = speedBox;
        chaseSpeed = chaseSpeedBox;
        moving = true;
    }

    private void Chase(Transform target)
    {
        if (isChasing)
        {
            // Calculate the direction towards the player
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f;   // Ignore height difference

            // Rotate towards the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            // Move towards the player
            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

            //Check if target is reached
            if(distanceToTarget <= targetReachedDistance) {
                ActionIfTargetIsReached();
            }
        }
    }

    public void CalculateDistanceToTarget(Transform target)
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
    }

    public void ActionIfTargetIsReached()
    {
        //Attack if Fox or InfectedChick
        if (predator)
        {
            Attack();
        }
        //Stop if Chick
        else if (prey)
        {
            StartCoroutine(Stop());
        }
   
    }

    public void Attack()
    {
        Debug.Log("Attack!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, targetReachedDistance);
    }


}

