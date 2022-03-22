using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisIA : MonoBehaviour
{
   

    [Header("PLAYERINTERRACTION")]
    private Transform player;
    public float distanceDeDetection;
    public float vitesse;
    public bool shootJoeur;
    //public PlayerHealth hpPlayer;
    //public GameObject shipPlayer;
    public LayerMask Avatar;
    //public GameObject pt;
    public bool raycastHit;
    //public GameObject Ship;


    [Header("GUN")]
    public Transform Blaster;
    public Transform firepoint;
    public GameObject bullet;
    public float BulletSpeed;
    public float spreadFactor;
    public float fireTime;
    public float fireRate;
    public float nextTimeToFire;


    [Header("Player")]
    public float dammage;

    



   

    // Start is called before the first frame update

 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //chopper une ref a la liste des cibles, y a surement une alternative au FindGameobject a voir avec Bruno
    }

  

    
   
    
    public void shoot()
    {
        Vector3 shootDir = firepoint.transform.forward;                     //pour tirer en face
        shootDir.x += Random.Range(-spreadFactor, spreadFactor);            //mais on ajoute un petit spread pour pas tirer tout droit non plus
        shootDir.y += Random.Range(-spreadFactor, spreadFactor);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Avatar))
            Debug.DrawRay(transform.position, transform.forward * 100, Color.green); print("Hit");
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Joueur touche");
            raycastHit = true;

            if (raycastHit == true)
			{
                nextTimeToFire = Time.time + fireRate;
			}

          
            if (hit.collider.GetComponent<ViePlayer>() != null)
                Debug.Log("Aie");
            {
                hit.collider.GetComponent<ViePlayer>().TakeDammage(dammage);
                

            }
        
        }

   
    }
     
   
    /// ////////////////////////////////////////// SI LA BULLET ENTER DANS LA TRIGER SUR JOUEUER LE JOUEUR PERD 1 PV////////////////////////

	

	// Update is called once per frame
	void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > distanceDeDetection) 
		{
            shootJoeur = false;
            //patrouille
        }
        else
		{
            //poursuite
            shootJoeur = true;
            

            transform.LookAt(player.position);
            transform.Translate(Vector3.forward * vitesse * Time.deltaTime); //Possibilite d'augmenter la vitesse pour ennemi kamikaze

		}

		

        if (shootJoeur == true && Time.time > nextTimeToFire)        // en fait un cooldown en gros
		{
            
            fireRate = Time.time + fireRate;

            if (fireRate >= 1 ) // c'est ici qu'on regle le CD des ennemis
            {
                shoot();
                Debug.Log("jetire"); 

            }


        }
    }
    
}
