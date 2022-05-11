using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public float motorForce;


    public WheelCollider w1;
    public WheelCollider w2;
    public WheelCollider w3;
    public WheelCollider w4;
    public WheelCollider w5;
    public WheelCollider w6;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            w1.motorTorque = 1 * motorForce;
            w2.motorTorque = 1 * motorForce;
            w3.motorTorque = 1 * motorForce;
            w4.motorTorque = 1 * motorForce;
            w5.motorTorque = 1 * motorForce;
            w6.motorTorque = 1 * motorForce;
        }
        else
        {
            w1.motorTorque = 0;
            w2.motorTorque = 0;
            w3.motorTorque = 0;
            w4.motorTorque = 0;
            w5.motorTorque = 0;
            w6.motorTorque = 0;
        }
    }
    
}
