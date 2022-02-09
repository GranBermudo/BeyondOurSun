using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saut2 : MonoBehaviour
{
    private Rigidbody rb;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    public int direction;
	public float StopDash;
	public PropellerShipBehaviour PropellerShipBehaviourscript;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		dashTime = startDashTime;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Xbutton"))
		{
			rb.velocity = Vector3.forward * dashSpeed;
			Debug.Log("nahbouk");
		}
		else PropellerShipBehaviourscript.Speed -= Mathf.Sign(PropellerShipBehaviourscript.Speed) * PropellerShipBehaviourscript.decelerationFactor * Time.deltaTime;
		
	}






	/* public float speed = 10f;
	 public float dashLength = 0.15f;
	 public float dashSpeed = 100f;
	 public float dashResetTime = 1f;

	 public CharacterController characterController;

	 private Vector3 dashMove;
	 private float dashing = 0f;
	 private float dashingTime = 0f;
	 private bool canDash = true;
	 private bool dashingNow = false;
	 private bool dashReset = true;

	 void Update()
	 {
		 float moveX = Input.GetAxis("Horizontal");
		 float moveZ = Input.GetAxis("Vertical");

		 Vector3 move = transform.right * moveX + transform.forward * moveZ;

		 if (move.magnitude > 1)
		 {
			 move = move.normalized;
		 }


		 if (Input.GetButtonDown("Xbutton") == true && dashing < dashLength && dashingTime < dashResetTime && dashReset == true && canDash == true)
		 {
			 dashMove = move;
			 canDash = false;
			 dashReset = false;
			 dashingNow = true;
		 }

		 if (dashingNow == true && dashing < dashLength)
		 {
			 characterController.Move(dashMove * dashSpeed * Time.deltaTime);
			 dashing += Time.deltaTime;
		 }

		 if (dashing >= dashLength)
		 {
			 dashingNow = false;
		 }

		 if (dashingNow == false)
		 {
			 characterController.Move(move * speed * Time.deltaTime);
		 }

		 if (dashReset == false)
		 {
			 dashingTime += Time.deltaTime;
		 }

		 if (characterController.isGrounded && canDash == false && dashing >= dashLength) //pas besoin de is grounded
		 {
			 canDash = true;
			 dashing = 0f;
		 }

		 if (dashingTime >= dashResetTime && dashReset == false)
		 {
			 dashReset = true;
			 dashingTime = 0f;
		 }
	 }*/
}