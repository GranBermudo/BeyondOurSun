using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Ship : MonoBehaviour
{

    public float baseSpeed;
 
    Rigidbody shipRigidbody;
    public float RotationSpeed;
    public float distanceToMove;
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = 50.0f;
        shipRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("LeftStickHorizontal");
        //float z = Input.GetAxis("RightStickVertical");
        //Vector3 movement = new Vector3(x, 0, z);
        //transform.Translate(movement * speed * Time.deltaTime);

        Debug.Log(Input.GetAxis("RightStickVertical"));
        if (Input.GetAxis("RightStickVertical") > 0.1 && baseSpeed > 0)                     //ici toute une partie sur le straffing qui fonctionnait pas comme je voulait mais 
        {                                                                                                        //ça serai bien de l'implémenter, voir avec Bruno
            baseSpeed = 60f;
        }

        if (Input.GetAxis("RightStickVertical") > 0.75 && baseSpeed >= 60)                     //ici toute une partie sur le straffing qui fonctionnait pas comme je voulait mais 
        {                                                                                                        //ça serai bien de l'implémenter, voir avec Bruno
            baseSpeed = 70f;
        }
        else if (Input.GetAxis("RightStickVertical") > -0.1 && baseSpeed > 0)
        {
            baseSpeed = 30f;
        }
    }
    private void FixedUpdate()
	{
        Vector3 move = new Vector3();

        //acceleration
        Vector3 acceleration = new Vector3(0, 0, baseSpeed) * Time.deltaTime;
        //Vector3 acceleration = new Vector3(CurrentSpeedStraffing.x, CurrentSpeedStraffing.y, Speed) * Time.deltaTime;           //la partie qui fait avancer ou reculer le vaisseau, ça n'utilise pas la physique
        transform.Translate(acceleration);

       



        //float Yrot = 0;

        if (distanceToMove > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);

        }

        //Vector3 rotate = new Vector3(0, Yrot, -Input.GetAxis("LeftStickHorizontal"));
        Vector3 rotate = new Vector3(0, -Input.GetAxis("LeftStickVertical"),0 );
        //move = rotate.normalized * Time.deltaTime * (RotationSpeed * 10);
        // shipRigidbody.AddRelativeTorque(move);     //la rotation est physiquée

      
    }
}
