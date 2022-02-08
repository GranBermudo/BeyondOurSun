using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerShipBehaviour : MonoBehaviour
{

    public Rigidbody rb;

    [Header("Ship Controls Parameters")]
    public float Speed = 0;
    public float maxSpeed;
    public float SpeedStraffing = 0;
    public float maxSpeedStraffing;
    public float dragStraffing;
    public float maxBoostSpeed;
    public float accelerationFactor;    //a quel point le vaisseau accelere ou decelere vite
    public float decelerationFactor;
    public float RotationSpeed;
    

    private Vector2 CurrentSpeedStraffing = Vector2.zero;
    //public float sautTP = Vector3.zero;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Debug.Log(Input.GetAxis("LT"));
        //Increase or decrease ship speed
        if (Input.GetAxis("LT") > 0 && Speed < maxSpeed)
        {
            Speed += accelerationFactor * Time.deltaTime;               //ici on augmente ou diminue la vitesse du vaisseau, elle redescend pas automatiquement mais
        }                                                               //pour avoir une maniabilité plus insectoide ça serai bien que si et que ça s'arrête plutôt vite même
        else if(Input.GetAxis("LT") < 0 && Speed > -maxSpeed)           //y avait un truc qui faisait ça dans ShipBehaviour regarde avec Brun au pire
        {
            Speed -= accelerationFactor * Time.deltaTime;
        }

        //to tsop the ship if there is not enough speed
        if (Input.GetAxis("LT") == 0)
        {
            Speed -= Mathf.Sign(Speed) * decelerationFactor * Time.deltaTime;
            if (Speed < 7 && Speed > -7)
            {
                Speed = 0;                                              //on remet la vitesse a zéro si elle est sous une certaine valeur et qu'on ne touche pas aux contrôles
            }
        }

       // if(Input.GetButton("Xbutton"))
		{
           // sautTP.z;

        }






        //increase or decrease vertical straffing speed
     
        if (Input.GetAxis("RightStickVertical") > 0.1 && CurrentSpeedStraffing.y < maxSpeedStraffing)                     //ici toute une partie sur le straffing qui fonctionnait pas comme je voulait mais 
        {                                                                                                        //ça serai bien de l'implémenter, voir avec Bruno
            CurrentSpeedStraffing.y += (accelerationFactor * 2) * Time.deltaTime;
        }
        else if (Input.GetAxis("RightStickVertical") < -0.1 && CurrentSpeedStraffing.y > -maxSpeedStraffing)
        {
            CurrentSpeedStraffing.y -= (accelerationFactor * 2) * Time.deltaTime;
        }
        else if(Input.GetAxis("RightStickVertical") > -0.1 && Input.GetAxis("RightStickVertical") < 0.1)
        {
            CurrentSpeedStraffing.y = 0;
        }

        if (Input.GetAxis("RightStickHorizontal") > 0.1 && CurrentSpeedStraffing.x < maxSpeedStraffing)                     //ici toute une partie sur le straffing qui fonctionnait pas comme je voulait mais 
        {                                                                                                        //ça serai bien de l'implémenter, voir avec Bruno
            CurrentSpeedStraffing.x += (accelerationFactor * 2) * Time.deltaTime;
        }
        else if (Input.GetAxis("RightStickHorizontal") < -0.1 && CurrentSpeedStraffing.x > -maxSpeedStraffing)
        {
            CurrentSpeedStraffing.x -= (accelerationFactor * 2) * Time.deltaTime;
        }
        else if (Input.GetAxis("RightStickHorizontal") > -0.1 && Input.GetAxis("RightStickHorizontal") < 0.1)
        {
            CurrentSpeedStraffing.x = 0;
        }

    }

    private void FixedUpdate()
    {
        /////USING A CONTROLLER/////

        Vector3 move = new Vector3();

        //acceleration
        Vector3 acceleration = new Vector3(CurrentSpeedStraffing.x, CurrentSpeedStraffing.y, Speed) * Time.deltaTime;           //la partie qui fait avancer ou reculer le vaisseau, ça n'utilise pas la physique
        transform.Translate(acceleration);                                          //avant c'était fait avec un AddForce mais je saurai pas dire pourquoi ça faisait drifter le vaisseau de fou
                                                                                    //ça serai bien de le re physiquer pour utiliser le drag du rigidbody
        //straffing
        /*if (SpeedStraffing != 0)
        {
            //c'était sensé straffer avec de la physique d'ou le AddRelativeForce
            //le drag du rigidbody aurai stoppé le straff
            rb.AddRelativeForce(-Vector3.up * SpeedStraffing * Time.deltaTime);     //cette partie sert a rien pour l'instant vu qu'y a pas de straffing mais ouais a voir
            CurrentSpeedStraffing += SpeedStraffing * Time.deltaTime;               //pour en mettre ça serai cool
        }
        else if(CurrentSpeedStraffing != 0)
        {
            rb.AddRelativeForce(-Vector3.up * CurrentSpeedStraffing * -1);
            CurrentSpeedStraffing = 0;
        } */

        //rotation
        float Yrot = 0;
        if (Input.GetButton("LB"))
        {
            Yrot = -1;
        }
        else if (Input.GetButton("RB"))                                             //ici ça fait tourner le vaisseau
        {
            Yrot = 1;
        }
        else if (Input.GetButton("LB") && Input.GetButton("RB"))
        {
            Yrot = 0;
        }

        Vector3 rotate = new Vector3(-Input.GetAxis("LeftStickVertical"), Yrot, -Input.GetAxis("LeftStickHorizontal"));
        move = rotate.normalized * Time.deltaTime * (RotationSpeed * 10);
        rb.AddRelativeTorque(move);     //la rotation est physiquée
    }
}
