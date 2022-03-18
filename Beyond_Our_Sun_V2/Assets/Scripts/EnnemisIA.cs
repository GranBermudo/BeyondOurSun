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
    public GameObject Ship;


    [Header("GUN")]
    public Transform Blaster;
    public Transform firepoint;
    public GameObject bullet;
    public float BulletSpeed;
    public float spreadFactor;
    public float fireTime;

    [Header("Player")]
    public float dammage;



    //private PropellerShipBehaviour PSBscript;
    //private SystemePlayerHealth hpPlayer;


    // Start is called before the first frame update

    private void Start()
    {
        //PSBscript = GetComponent<PropellerShipBehaviour>();     //reference au script du joueur pour les transform et les valeur de vitesse
       // fireTime = 1f;
       // hpPlayer = GetComponent<SystemePlayerHealth>();      //Reference au script des pv du joueur


    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //chopper une ref a la liste des cibles, y a surement une alternative au FindGameobject a voir avec Bruno
    }

   /* public void shootAutocannon(GameObject bullet, Transform firepoint, float BulletSpeed) // l'ennemis tire sur le joueur
    {
        Vector3 shootDir = firepoint.transform.forward;                     //pour tirer en face
        shootDir.x += Random.Range(-spreadFactor, spreadFactor);            //mais on ajoute un petit spread pour pas tirer tout droit non plus
        shootDir.y += Random.Range(-spreadFactor, spreadFactor);

        var projectileObj = Instantiate(bullet, firepoint.position, Blaster.rotation) as GameObject;                            //faire apparaitre la balle
        projectileObj.GetComponent<Rigidbody>().AddForce(shootDir * (BulletSpeed + PSBscript.Speed), ForceMode.Impulse);        //avec une vitesse initiale pour pas toucher notre vaisseau mais je pense 
    }*/

    
   
    
    public void shoot()
    {
        Vector3 shootDir = firepoint.transform.forward;                     //pour tirer en face
        shootDir.x += Random.Range(-spreadFactor, spreadFactor);            //mais on ajoute un petit spread pour pas tirer tout droit non plus
        shootDir.y += Random.Range(-spreadFactor, spreadFactor);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Avatar))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Joueur touche");
            raycastHit = true;

            //Target targ = GetComponent<Ship>();
            //Target targ = hit.collider.GetComponent<Ship>();

            // Target target = hit.transform.GetComponent<Target>();
            // if (target != null)
            //{
            ////    target.TakeDamage(damage);
            if (hit.collider.GetComponent<ViePlayer>() != null)
			{
                hit.collider.GetComponent<ViePlayer>().TakeDammage(dammage);

            }
        
        }

        

        //if (raycastHit == true && hit.transform.gameObject == Ship) 
		
            //Pv.hpPlayer
           // Damage();
        


        /*void Damage()
		{
            // hpPlayer.playerHealth = hpPlayer.playerHealth - raycastHit;
            hpPlayer.playerHealth = hpPlayer.playerHealth - raycastHit == true && hit.transform.gameObject == Ship;
            hpPlayer.UpdateHealth();
            gameObject.SetActive(false);*/

        




        //var projectileObj = Instantiate(bullet, firepoint.position, Blaster.rotation) as GameObject;                            //faire apparaitre la balle
        //projectileObj.GetComponent<Rigidbody>().AddForce(shootDir * (BulletSpeed), ForceMode.Impulse);
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

		if (fireTime < 0)         //si fire time est egale a 0 ça va se remettre a 1 et ça reshoot que quand c'est a 1
		{
            fireTime = 1;
		}

        if (shootJoeur == true )        // en fait un cooldown en gros
		{
            fireTime -= Time.deltaTime;

            if (fireTime >= 0.9) 
            {
                shoot();
                Debug.Log("jetire"); 

            }

           // RaycastHit hit;
           /* Ray ray = new Ray(transform.position, Vector3.forward);

            int layer_mask = LayerMask.GetMask("Avatar");

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask, QueryTriggerInteraction.Ignore))
			{
                print(hit.transform.name + "travers le rayon");
                print("la distance est de" + hit.distance);
                pt.transform.position = hit.point;
			}*/


            /*if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, Avatar))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }

            {
                Vector3 fwd = transform.TransformDirection(Vector3.forward);

                if (Physics.Raycast(transform.position, fwd, 10))
                    print("There is something in front of the object!");
            }*/

        }
    }
    
}
