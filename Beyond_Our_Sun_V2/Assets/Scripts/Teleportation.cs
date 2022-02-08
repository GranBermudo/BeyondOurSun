using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{

    public Vector3 moveDirection;

    public const float maxDashTime = 1.0f;
    public float dashDistance = 50;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;
    PropellerShipBehaviour PropellerShipBehaviourscript;



    private void Awake()
    {
        PropellerShipBehaviourscript = GetComponent<PropellerShipBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Xbutton")) //Right mouse button
        {
            currentDashTime = 0;
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
       // PropellerShipBehaviourscript.Move(moveDirection * Time.deltaTime * dashSpeed);
    }


   /* public Transform player;
    public float saut;
    public float DashForce;
    public float DragForce;
    // Start is called before the first frame update*/
   /* void Start()
    {
        
    }*/

    // Update is called once per frame

    
       /* if (Input.GetButton("Xbutton"))
		{
            saut transform.position
                _velocity += Vector3.Scale(transform.forward, DashForce * new Vector3((Mathf.Log(1f / (Time.deltaTime * DragForce.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * DragForce.z + 1)) / -Time.deltaTime)));
        }*/
    
}
