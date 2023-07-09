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
    public float minChaseSpeed;
    public int attackDamage;
    public int rotationSpeed;
    public float targetReachedDistance;
    public float chaseRange;
    public float attackCooldown;

    [Header("*Variables*")]
    public int health;
    public Vector3 targetDirection;
    public float speedBox;
    public float chaseSpeedBox;
    public Transform chaseTarget;
    public float distanceToTarget;
    public bool moving = true;
    public bool attackIsCool = true;
    public bool dead;

    [Header("*States*")]
    public bool isWandering;
    public bool isChasing;
    public bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        health= maxHealth;
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
        if (dead == true)
        {
            StartCoroutine(Death());
        }

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
        float angle = Random.Range(-60, 360);
        targetDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
    }
    public IEnumerator Stop()
    {
        if (moving)
        {
            reScanTrigger();
            moving = false;
            speed = 0;
            chaseSpeed = 0;
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
            StartWalking();
        }
    }

    public void StartWalking()
    {
        speed = speedBox;
        chaseSpeed = chaseSpeedBox;
        moving = true;
        reScanTrigger();
    }

    public void Chase(Transform target)
    {
        if (isChasing)
        {
            // Calculate the direction towards the target
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f;   // Ignore height difference

            // Rotate towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * Random.Range(4, 7));

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
        if (distanceToTarget > chaseRange)
        {
            chaseTarget = null;
        }
    }

    public void ActionIfTargetIsReached()
    {
        //Attack if Predator or InfectedChick
        if (predator)
        {
            StartCoroutine(Attack());
        }
        //Stop if Chick
        else if (prey)
        {
            StartCoroutine(Stop());
            reScanTrigger();
        }

    }

    public IEnumerator Attack()
    {
        if (attackIsCool)
        {
            attackIsCool = false;
           // Debug.Log("Attack!");
            //Stop for a Second to play Animation?

            Player player = chaseTarget.GetComponent<Player>();
            Prey prey = chaseTarget.GetComponent<Prey>();
            Predator predator = chaseTarget.GetComponent<Predator>();

            //Inflict Damage to the target
            if (player != null)
            {
                chaseTarget.GetComponent<Player>().TakeDamage(attackDamage);
            }
            else if (prey != null)
            {
                chaseTarget.GetComponent<Prey>().TakeDamage(attackDamage);
            }
            else if (predator != null)
            {
                chaseTarget.GetComponent<Predator>().TakeDamage(attackDamage);
            }

            //Cooldown
            yield return new WaitForSeconds(attackCooldown);
            attackIsCool = true;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        //Make the entity slower the more damaged it gets
        if (prey && chaseSpeed > 3)
        {
            chaseSpeed = chaseSpeed - (chaseSpeed * 0.4f);
        }
        if (health <= 0)
        {
            dead = true;
        }

        reScanTrigger();
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetReachedDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void reScanTrigger()
    {
        if (!prey)
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = true;
        }
    }

}

