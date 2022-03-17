using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Ship : MonoBehaviour
{

    public float baseSpeed;
    Rigidbody shipRigidbody;
    ///public CharacterController controller; Trouver comment le replacer  https://www.youtube.com/watch?v=4HpC--2iowE

    public float distanceToMove;
    

    public float yRot;
    public float RotationSpeed;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    




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



        Input.GetAxis("LeftStickHorizontal");
        Input.GetAxis("LeftStickVertical");

        yRot += Time.deltaTime * RotationSpeed;


        float vertical = Input.GetAxis("LeftStickkVertical");
        float horizontal = Input.GetAxis("LeftStickHorizontal");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if ( direction.magnitude >= 0.1f)
		{
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //controller.Move(direction * baseSpeed * Time.deltaTime); TROUVER COMMENT LE REMPLACER
		}

        //transform.rotation = Quaternion.Euler(0, yRot, 0);





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
