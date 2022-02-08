using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisIA : MonoBehaviour
{
    private PropellerShipBehaviour PSBscript;
    

    private Transform player;
    public float distanceDeDetection;
    public float vitesse;
    
    public Transform Blaster;

    public bool shootJoeur;

    public Transform firepoint;
    public GameObject bullet;
    public float BulletSpeed;
    public float spreadFactor;
    public float fireTime;



    // Start is called before the first frame update

    private void Start()
    {
        PSBscript = GetComponent<PropellerShipBehaviour>();     //reference au script du joueur pour les transform et les valeur de vitesse
        fireTime = 1f;
        
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

        var projectileObj = Instantiate(bullet, firepoint.position, Blaster.rotation) as GameObject;                            //faire apparaitre la balle
        projectileObj.GetComponent<Rigidbody>().AddForce(shootDir * (BulletSpeed), ForceMode.Impulse);
    }
   
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
            

        }
    }
    
}
