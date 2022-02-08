using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private PropellerShipBehaviour PSBscript;

    [Header("Machineguns")]
    public Transform Blaster;
    public GameObject Bullet;
    public float BulletSpeed;
    public float fireRate = 15f;
    public float spreadFactor;
    private float nextTimeToFire = 0f;

    [Header("Missiles")]
    public Transform MissileLaucherTransform;
    public GameObject Missile;

    [Header("Targeting")]
    public LayerMask ennemiLayer;
    public float detectionRange = 50f;
    public List<GameObject> TargetsInSight = new List<GameObject>();    //la liste des cibles dispos, c'est le script AddToTargetList qui ajoute les cibles a la liste
    private int whereInList = 0;    //l'index qui indique sur quel membre de la liste on se situe
    public GameObject LockedShip;
    public GameObject lockedWidget;
    public GameObject GizmoLock;

    private void Start()
    {
        PSBscript = GetComponent<PropellerShipBehaviour>();     //reference au script du joueur pour les transform et les valeur de vitesse
    }

    // Update is called once per frame
    void Update()
    {
        //shooting with autocannon
        if (Input.GetButton("Abutton") && Time.time >= nextTimeToFire)      //ça tire tout les x temps qui est défini a la ligne juste en dessous, ça fonctionne
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            shootAutocannon(Bullet, Blaster, BulletSpeed);
        }

        //shooting a missile
        if (Input.GetButtonDown("Bbutton"))
        {
            //cooldownMissile
            shootMissile(Missile, MissileLaucherTransform);
        }

        //lock on an object from the target list
        if (Input.GetButtonDown("Ybutton"))
        {
            LockTarget();
        }

        //add cursor to locked ship and keep it facing Player
        if (LockedShip != null)                                   //!=   si c'est inégal ou Compare si deux objets font référence à un objet différent.  comprend pas le null 
        {
            lockedWidget.transform.LookAt(transform);               //ici ça fait en sorte que l'indicateur de verrouillage soit toujours face au joueur
            //GizmoLock.transform.LookAt(LockedShip.transform);                 //le gizmo lock c'est un indicateur dans le cockpit pour indiquer si la cible est derrière nous
                                                                                //a remettre si on se met en vue cockpit
            lockedWidget.transform.position = LockedShip.transform.position;        //placer l'indicateur de verouillage sur le vaisseau ennemi et l'activer
            lockedWidget.SetActive(true);
        }
        else
        {
            lockedWidget.SetActive(false);                                              //si on ne cible plus rien l'indicateur se désactive et retourne a l'origin
            lockedWidget.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void shootAutocannon(GameObject bullet, Transform firepoint, float BulletSpeed)
    {
        Vector3 shootDir = firepoint.transform.forward;                     //pour tirer en face
        shootDir.x += Random.Range(-spreadFactor, spreadFactor);            //mais on ajoute un petit spread pour pas tirer tout droit non plus
        shootDir.y += Random.Range(-spreadFactor, spreadFactor);

        var projectileObj = Instantiate(bullet, firepoint.position, Blaster.rotation) as GameObject;                            //faire apparaitre la balle
        projectileObj.GetComponent<Rigidbody>().AddForce(shootDir * (BulletSpeed + PSBscript.Speed), ForceMode.Impulse);        //avec une vitesse initiale pour pas toucher notre vaisseau mais je pense 
    }                                                                                                                           //plus que ce soit utile parce que j'ai touché aux collission entre layers
                                                                                                                                //pour éviter ça
    void shootMissile(GameObject missile, Transform MissileLauncher)
    {
        var projectileObj = Instantiate(missile, MissileLauncher.position, MissileLauncher.rotation) as GameObject;             //voilà ici on fait pop un missile
        if(PSBscript.Speed > 0)
        {
            projectileObj.GetComponent<Missile>().speed += PSBscript.Speed;     //pour pas qu'il aille moins vite que notre vaisseau on lui ajoute la vitesse du vaisseau
        }
        if (LockedShip != null)
        {
            projectileObj.GetComponent<Missile>().target = LockedShip.transform;    //si on a une cible on l'indique au missile 
        }
        projectileObj.GetComponent<Missile>().initiateTrackingDelay();      //le missile suis la cible après un certain temps et ici on active le timer pour ça
    }

    void LockTarget()
    {
        //iterate trough target list        //ici c'est tricky c'est pour passer a l'élément suivant dans la liste de selection de cible

        whereInList++;  //passer a l'élément de la liste suivant en ajoutant +1 a l'indexe (++ est identique à +1)

        if (TargetsInSight.Count != 0)  //si la liste n'est pas vide
        {
            if (whereInList <= TargetsInSight.Count - 1)        //si on ne dépasse pas la taille de la liste, les listes commencent a zéro du coupe l'élément indexé 2 sera en fait le troisième membre de la liste sauf que le .Count dira que la liste contient trois membre donc pour combler le décalage je rajoute -1  ;)
            {
                if (TargetsInSight[whereInList] != null)    //si la case de la liste donnée par l'indexe n'est pas vide
                {
                    LockedShip = TargetsInSight[whereInList];   //alors on désigne l'objet dans la case comme la nouvelle cible
                }
                else if (TargetsInSight[whereInList] == null)   //mais si la case est vide
                {
                    TargetsInSight[whereInList] = TargetsInSight[TargetsInSight.Count - 1]; //on envoie la case vide a la fin de la liste
                    TargetsInSight.RemoveAt(TargetsInSight.Count - 1);                      //puis on la supprime pour éviter les cases vides
                }
            }
            else if (whereInList > TargetsInSight.Count - 1)        //si on dépasse la taille de la liste
            {
                whereInList = 0;    //l'indexe retourne au début de la liste
                if (TargetsInSight[whereInList] != null)
                {
                    LockedShip = TargetsInSight[whereInList];       //meme logique qu'avant on désigne une nouvelle cible si la case n'est pas vide
                }                                                   //sinon on la supprime
                else if (TargetsInSight[whereInList] == null)
                {
                    TargetsInSight[whereInList] = TargetsInSight[TargetsInSight.Count - 1];
                    TargetsInSight.RemoveAt(TargetsInSight.Count - 1);
                }
            }
        }

        //unlock
        if (TargetsInSight.Count == 0)      //si la liste est vide alors y a pas de vaisseau ciblé
        {
            LockedShip = null;
        }
    }
}
