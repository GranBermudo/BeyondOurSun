using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{

   /* public GameObject PlayerShip;
    var rememberMe : GameObject;
    var rememberMeLocation : Vector3;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    function Start()
    {
        rememberMe = GameObject.Find("PlayerShip");
    }
    // Place right before the object is destroyed
    rememberMeLocation = Vector3(rememberMe.transform.position.x, rememberMe.transform.position.y, rememberMe.transform.position.z);
    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    
    // each box would have a script like this:
   /* void Awake()
    {
        Invoke("SpawnNext", 2f);
    }
    void SpawnNext()
    {
        GameObject newBox = Instantiate(PlayerShip);// each box would have a script like this:
        void Awake()
        {
            Invoke("SpawnNext", 2f);
        }
        void SpawnNext()
        {
            GameObject newBox = Instantiate(PlayerShip);
            newBox.transform.position = new Vector3(position.x, position.y + transform.localScale.y / 2, position.z);
        }
        newBox.transform.position = new Vector3(position.x, position.y + transform.localScale.y / 2, position.z);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Xbutton"))
        {
           // rb.velocity = Vector3.forward * dashSpeed;
           // Debug.Log("nahbouk");
        }
       // else PropellerShipBehaviourscript.Speed -= Mathf.Sign(PropellerShipBehaviourscript.Speed) * PropellerShipBehaviourscript.decelerationFactor * Time.deltaTime;

    }
   // character.transform.position += character.transform.forward* desiredDistance;  <----------- IMPORTANT
}


/* cache le game object mais ça serait peut etre mieux de le detruire
 * 
GameObject cat;
cat.SetActive(false); // false to hide, true to show

cat.GetComponent<Renderer>().enabled = false;

MeshRenderer mr = go.GetComponent<MeshRenderer>();
mr.enabled = false;


Destroy(this.gameObject);


function Spawn() {
 
 if(Input.GetButton("Fire1")) { // action that needs to be done in order to
 // instantiate a new prefab
 
 Instantiate(Object, Spawn.position , Spawn.rotation);
 // instantiates a new prefab (Instantiate(objecttospawn, spawnposition. 
 // spawnrotation)
 Yield WaitForSeconds(WaitTime);
 // makes the code wait WaitTime and then resumes
 Destroy(gameObject);
 // destroys the game object*/
}

//faire ça avec un collider ou raycast