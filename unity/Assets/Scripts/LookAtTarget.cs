﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LookAtTarget : MonoBehaviour {

    public Vector2 targetPos = Vector2.zero;

    public Rigidbody2D rb;

    public bool usePhysics = true;

    public bool rotateInUpdate = false;
    

	// Use this for initialization
	void Start () {
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (rotateInUpdate && !usePhysics)
        {
            RotateWithoutPhysics();
        }
	}

    public Vector2 offset = Vector2.zero;
    public float angle = 0.0f;
    public float deltaAngle = 0.0f;
    public float reverseDelta = 0.0f;

    public float turnSpeed = 10.0f;

    public float rbRotation = 0.0f;

    public float resultantRotation = 0.0f;

    public List<Rigidbody2D> addlRbAdjust = new List<Rigidbody2D>();

    void FixedUpdate()
    {
        if (rotateInUpdate && usePhysics)
        {
            RotateWithPhysics();
            

            //rb.MoveRotation(rb.rotation + (deltaAngle) * turnSpeed * Time.fixedDeltaTime);


        }
        
    }

    public void RotateWithoutPhysics()
    {
        Vector3 thisPos = transform.localPosition;
        Vector2 offset = new Vector2(targetPos.x - thisPos.x, targetPos.y - thisPos.y);

        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void RotateWithPhysics()
    {
        Vector3 thisPos = transform.localPosition;

        offset = new Vector2(targetPos.x - thisPos.x, targetPos.y - thisPos.y);

        //bool turnRight = offset.x > 0.0f;

        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        deltaAngle = angle - rb.rotation;
        //reverseDelta = rb.rotation - angle;

        if (deltaAngle >= 180.0f)
        {
            deltaAngle = deltaAngle - 360.0f;
        }
        else if (deltaAngle <= -180.0f)
        {
            deltaAngle = deltaAngle + 360.0f;
        }
        //Vector3 eulerAngleVelocity = new Vector3(0.0f, 0.0f, angle);
        //Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * 1.0f);

        rbRotation = rb.rotation;



        if (rbRotation > 360.0f)
        {
            rb.rotation = rbRotation - 360.0f;
            foreach (Rigidbody2D addlRB in addlRbAdjust)
            {
                addlRB.rotation = addlRB.rotation - 360.0f;
            }
        }
        else if (rbRotation < -360.0f)
        {
            rb.rotation = rbRotation + 360.0f;
            foreach (Rigidbody2D addlRB in addlRbAdjust)
            {
                addlRB.rotation = addlRB.rotation + 360.0f;
            }
        }

        resultantRotation = rb.rotation + (deltaAngle * turnSpeed * Time.fixedDeltaTime);


        rb.MoveRotation(resultantRotation);
    }
}
