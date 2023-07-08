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
    public float chaseRange;

    [Header("*Variables*")]
    public int health;
    public Vector3 targetDirection;
    public float speedBox;
    public float chaseSpeedBox;
    public Transform chaseTarget;
    public float distanceToTarget;
    public bool moving = true;

    [Header("*States*")]
    public bool isWandering;
    public bool isChasing;
    public bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        speedBox = speed;
        chaseSpeedBox = chaseSpeed;

        if (predator)
        {
            isWandering = true;
        }
        else if (prey)
        {
            chaseTarget = GameObject.Find("Player").transform;

            isChasing = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (chaseTarget != null)
        {
            CalculateDistanceToTarget(chaseTarget);
        }
        WanderAround();
        if (chaseTarget == null)
        {
            isChasing = false;
            isWandering = true;
        }
        else
        {
            Chase(chaseTarget);
            
        }

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

    public void ChangeDirection()
    {
        // Generate a new random direction
        float angle = Random.Range(0, 360);
        targetDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
    }
    public IEnumerator Stop()
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

    public void StartWalking()
    {
        speed = speedBox;
        chaseSpeed = chaseSpeedBox;
        moving = true;
    }

    public void Chase(Transform target)
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
            if (distanceToTarget <= targetReachedDistance)
            {
                ActionIfTargetIsReached();
            }
        }
    }

    public void CalculateDistanceToTarget(Transform target)
    {

        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if(distanceToTarget > chaseRange)
        {
            chaseTarget = null;
        }
    }

    public void ActionIfTargetIsReached()
    {
        //Attack if Predator or InfectedChick
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetReachedDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }



}

