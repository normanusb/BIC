using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    private Vector3 playerModelAnchorPos;
    private Quaternion playerModelAnchorRot;
    [Space(20)]

    [Header("Animation Settings")]
    [Header("ONLY TOUCH WITH JULIANS APPROVAL!")]
    public float swayAngle = 7f;
    public float swaySpeed = 2f;

    private float zRot = 0;
    private float yRot = 0;
    private bool swayDir = true;
    private float swayTime = 0f;
    private Vector3 velocity;

    private Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        velocity = lastPos - transform.position;
        velocity.y = 0;

        swayTime += Time.deltaTime * (1f + velocity.magnitude * 50f);

        if (swayTime >= 1f / swaySpeed)
        {
            swayDir = !swayDir;
            swayTime = 0f;
        }

        yRot = swayDir ? swayAngle : -swayAngle;
        playerModelAnchorRot = Quaternion.Euler(0, zRot, yRot);

        float f = Time.deltaTime * 10f;
        if (f > 1f)
        {
            f = 1f;
        }
        transform.localRotation = Quaternion.Slerp(transform.localRotation, playerModelAnchorRot, f);
        lastPos = transform.position;
    }

}
