using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("*Adjustable Stats*")]
    public int maxHealth;
    public float speed;
    public int attackDamage;
    public int rotationSpeed;

    [Header("*Variables*")]
    public int health;
    private Vector3 targetDirection;
    private float speedBox;

    [Header("*States*")]
    public bool isWandering;
    public bool isChasing;
    public bool isFleeing;
    public bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        speedBox = speed;
        isWandering = true;
    }

    // Update is called once per frame
    void Update()
    {
        WanderAround();
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
        speed = 0;
        yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));
        StartWalking();
    }

    private void StartWalking()
    {
        speed = speedBox;
    }
}

