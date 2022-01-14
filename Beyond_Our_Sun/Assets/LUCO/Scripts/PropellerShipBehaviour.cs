using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerShipBehaviour : MonoBehaviour
{
    public static PropellerShipBehaviour propellerShipScript;

    public Rigidbody rb;

    [Header("Ship Controls Parameters")]
    public float Speed = 0;
    public float maxSpeed;
    public float SpeedStraffing = 0;
    public float maxSpeedStraffing;
    public float dragStraffing;
    public float maxBoostSpeed;
    public float accelerationFactor;
    public float RotationSpeed;

    private float CurrentSpeedStraffing = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Increase or decrease ship speed
        if (Input.GetAxis("LT") > 0 && Speed < maxSpeed)
        {
            Speed += accelerationFactor * Time.deltaTime;
        }
        else if(Input.GetAxis("LT") < 0 && Speed > -maxSpeed)
        {
            Speed -= accelerationFactor * Time.deltaTime;
        }

        //to tsop the ship if there is not enough speed
        if (Input.GetAxis("LT") == 0)
        {
            if (Speed < 7 && Speed > -7)
            {
                Speed = 0;
            }
        }


        //increase or decrease vertical straffing speed
        /*
        if (Input.GetAxis("RightStickVertical") > 0.1 && SpeedStraffing < maxSpeedStraffing)
        {
            SpeedStraffing += (accelerationFactor * 2) * Time.deltaTime;
        }
        else if (Input.GetAxis("RightStickVertical") < -0.1 && SpeedStraffing > -maxSpeedStraffing)
        {
            SpeedStraffing -= (accelerationFactor * 2) * Time.deltaTime;
        }
        else if(Input.GetAxis("RightStickVertical") > -0.1 && Input.GetAxis("RightStickVertical") < 0.1)
        {
            SpeedStraffing = 0;
        }
        */
    }

    private void FixedUpdate()
    {
        /////USING A CONTROLLER/////

        Vector3 move = new Vector3();

        /*
        //acceleration
        acceleration = new Vector3(0, 0, Input.GetAxisRaw("LT"));
        acceleration = acceleration.normalized * Time.deltaTime * Speed;
        rb.AddRelativeForce(acceleration, ForceMode.Acceleration);
        */

        //acceleration
        Vector3 acceleration = new Vector3(0, 0, Speed) * Time.deltaTime;
        transform.Translate(acceleration);

        //straffing
        if (SpeedStraffing != 0)
        {
            rb.AddRelativeForce(-Vector3.up * SpeedStraffing * Time.deltaTime);
            CurrentSpeedStraffing += SpeedStraffing * Time.deltaTime;
        }
        else if(CurrentSpeedStraffing != 0)
        {
            rb.AddRelativeForce(-Vector3.up * CurrentSpeedStraffing * -1);
            CurrentSpeedStraffing = 0;
        }

        //rotation
        float Yrot = 0;
        if (Input.GetButton("LB"))
        {
            Yrot = -1;
        }
        else if (Input.GetButton("RB"))
        {
            Yrot = 1;
        }
        else if (Input.GetButton("LB") && Input.GetButton("RB"))
        {
            Yrot = 0;
        }

        Vector3 rotate = new Vector3(-Input.GetAxis("LeftStickVertical"), Yrot, -Input.GetAxis("LeftStickHorizontal"));
        move = rotate.normalized * Time.deltaTime * (RotationSpeed * 10);
        rb.AddRelativeTorque(move);
    }
}
