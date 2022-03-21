using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;




public class Controller_Ship : MonoBehaviour
{
    
    public float baseSpeed;
    Rigidbody shipRigidbody;
    ///public CharacterController controller; Trouver comment le replacer  https://www.youtube.com/watch?v=4HpC--2iowE
   

    public float rotationSpeed ;

    public float turnSmoothTime ;  //temps en seconde pour atteindre la nouvelle rotation (vitesse de rotation en gros)
    public float turnSmoothVelocity;
    public float turnSmoothVelocity2;

    public bool isDashing;
    public float dashSpeed;
    public float dashTime;
    private float dashTimer;
    private Vector3 dashDir;

    public Transform proxyRotation;
    public float vitesseReset;

    //PostProcessVolume m_Volume;
    //LensDistortion MyMLensDistortion;
    [SerializeField] public PostProcessVolume volume;
    public LensDistortion LensDistortionEffect = null;

       // enable post process
   




    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = 50.0f;
        shipRigidbody = GetComponent<Rigidbody>();
        //MyMLensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
       // MyMLensDistortion.intensity.Override(1f);

        
        volume.profile.TryGetSettings(out LensDistortionEffect);
        LensDistortionEffect.active = false;

        //LensDistortionEffect.active = true;
    }

    // Update is called once per frame
  
    private void FixedUpdate()
	{

        if (isDashing == false)
        {

            //Debug.Log(Input.GetAxis("RightStickVertical"));
            if (Input.GetAxis("RightStickVertical") > 0.1)
            {
                baseSpeed = 60f;
                LensDistortionEffect.active = false;
            }

            if (Input.GetAxis("RightStickVertical") > 0.75)
            {
                baseSpeed = 80f;
                LensDistortionEffect.active = true;
            }

            if (Input.GetAxis("RightStickVertical") < 0.1)
            {
                baseSpeed = 60f;
            }

            if (Input.GetAxis("RightStickVertical") < -0.1)
            {
                baseSpeed = 30f;

            }
        }



        //Debug.Log(Input.GetAxis("LeftStickHorizontal"));
        //Debug.Log(Input.GetAxis("LeftStickVertical"));
        float vertical = Input.GetAxis("LeftStickVertical");
        float horizontal = Input.GetAxis("LeftStickHorizontal");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (isDashing == false)
        {

            if (direction.magnitude >= 0.1f)
            {
                transform.Rotate(Vector3.up * rotationSpeed * direction.x * Time.deltaTime * 50f);          //Rotation
                transform.Rotate(Vector3.right * rotationSpeed * direction.z * Time.deltaTime * 50f);


            }

            else   //Reset la rotation a celle de base en X et Z
            {
                Quaternion newRotation = transform.rotation;
                Vector3 eulerRotation = newRotation.eulerAngles;
                eulerRotation.x = 0;
                eulerRotation.z = 0;
                proxyRotation.eulerAngles = eulerRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, proxyRotation.rotation, vitesseReset * Time.deltaTime * 50f);


            }

        }

        bool Gauche = Input.GetButtonDown("LB");
        bool Droit = Input.GetButtonDown("RB");

        if (isDashing == false)
        {
            if (Gauche == true)
            {
                isDashing = true;
                dashTimer = dashTime;
                dashDir = Vector3.right * -1;

            }
            else if (Droit == true)
            {
                isDashing = true;
                dashTimer = dashTime;
                dashDir = Vector3.right;
            }

        }


        if (isDashing == true)
		{
            dashTimer -= Time.deltaTime;

            if(dashTimer >0)
			{
                transform.Translate(dashDir * dashSpeed);

            }

            else
			{
                isDashing = false;
			}
            

		}
       else
		{
            Vector3 acceleration = new Vector3(0, 0, baseSpeed) * Time.deltaTime;
            transform.Translate(acceleration);

        }

    }
}
