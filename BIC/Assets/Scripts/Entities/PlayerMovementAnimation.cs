using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    public Transform playerModel;
    public Transform playerModelAnchor;
    private Transform targetTransform;
    [Space(20)]

    [Header("Animation Settings")]
    [Header("ONLY TOUCH WITH JULIANS APPROVAL!")]
    public float hopThreshold = 0.3f;
    public float hopHeight = 0.2f;
    public float airTimeLimit = 0.1f;


    private float zRot = 0;
    private bool onGround = true;
    private float hopCooldown = 0.1f;
    private float idleTime = 0f;

    private Vector3 targetPos
    {
        get { return targetTransform.localPosition; }
        set { targetTransform.localPosition = value; }
    }
    private Vector3 pos
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }

    void Start()
    {
        targetTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //UpdateInput(); // for testing

        UpdateAngle();
        UpdateMovement();
        if (hopCooldown > 0) { hopCooldown -= Time.deltaTime; }

        float f = Time.deltaTime * 30f;
        if(f > 1f) { f = 1f; }

        playerModel.localPosition = Vector3.Lerp(playerModel.localPosition, playerModelAnchor.localPosition, f);
        playerModel.localRotation = Quaternion.Slerp(playerModel.localRotation, playerModelAnchor.localRotation, f);
    }

    private void UpdateAngle()
    {
        Vector3 velo = targetPos - pos;
        velo.y = 0;
        if (velo.magnitude > 0.1f)
        {
            float a = Quaternion.LookRotation(velo).eulerAngles.y;

            Debug.Log(a);
            zRot = a;
        }
    }

    private void UpdateInput()
    {
        float speed = 8;
        var v = targetPos;
        if (Input.GetKey(KeyCode.A))
        {
            v.x -= Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            v.x += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            v.z += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            v.z -= Time.deltaTime * speed;
        }
        targetPos = v;
    }

    private bool idle = false;
    private void UpdateMovement()
    {
        float d = Vector3.Distance(targetTransform.localPosition, transform.localPosition);

        if (d < hopThreshold && d > 0.0001f)
        {
            idleTime += Time.deltaTime;
            if (idleTime >= airTimeLimit && !idle)
            {
                onGround = true;
                idle = true;
                Hop();
            }
        }
        else
        {
            idle = false;
            idleTime = 0;
        }

        if (Vector3.Distance(targetTransform.localPosition, transform.localPosition) > (onGround ? hopThreshold : hopThreshold / 2f))
        {
            if (hopCooldown <= 0)
            {
                hopCooldown = 0.03f;
                onGround = !onGround;
                Hop();
            }
        }
    }



    private void Hop()
    {
        pos = targetPos;
        playerModelAnchor.localPosition = new Vector3(0, onGround ? 0f : hopHeight, R(0.1f));
        playerModelAnchor.localRotation = Quaternion.Euler(onGround ? 0 : R(15f), zRot, onGround ? 0 : R(10f));
    }

    private float R(float r)
    {
        return UnityEngine.Random.Range(-r, r);
    }
}
