using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerShipBehaviour : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Ship Controls Parameters")]
    public float Speed = 0;
    public float maxSpeed;
    public float maxBoostSpeed;
    public float accelerationFactor;
    public float SpeedStraffing;
    public float RotationSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Increase or decrease ship speed
        if (Input.GetAxis("LT") != 0 && Speed < maxSpeed)
        {
            Speed += (accelerationFactor * Input.GetAxisRaw("LT")) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
