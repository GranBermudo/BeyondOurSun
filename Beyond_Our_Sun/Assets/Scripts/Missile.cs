using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;

    public float speed;
    public float turnRate;
    private Quaternion GuideRotation;

    public bool tracking;

    public ParticleSystem explosion;

    [SerializeField]
    private float trackingDelay;

    public float degats;
    public int layerNumber;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GuideMIssile(); //appeler une fonction qui calcule un vecteur pour faire tourner le missile vers la cible choisie dans la liste des cibles du WeaponSystem

        rb.velocity = transform.forward * speed;    //donner une vitesse au missile (a laquelle est rajoutée celle du vaisseau depuis un autre script)
        
        if(target != null && tracking == true)      //si le missile à une cible et que le timer de guidage est fini
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, GuideRotation, turnRate * Time.deltaTime);    //alors il se tourne vers la cible a une certaine vitesse donnée par turnRate
        }

        StartCoroutine(AutoDestruction());  //démarrer le timer d'autodestruction du missile
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerNumber)    //si le GameObject avec lequel le missile entre en collision est sur le layer 9
        {
            other.gameObject.GetComponent<Health>().TakeDammage(degats); //infliger des dégats par la suite
            Debug.Log(other.gameObject.name + "hit by missile");
            Instantiate(explosion, transform.position, Quaternion.identity);    //faire apparaitre un fx d'explosion
            Destroy(this.gameObject);   //et détruire le missile
        }
    }

    void GuideMIssile()
    {
        if (target == null) return;
        else
        {
            Vector3 relativePosition = target.position - transform.position;
            GuideRotation = Quaternion.LookRotation(relativePosition, transform.up);
        }
    }

    public void initiateTrackingDelay() //c'est la fonction qu'on appelle depuis le WeaponSystem
    {
        StartCoroutine(TargetTrackingDelay());  //elle demarre un countdown pour le guidage vers la cible
    }

    IEnumerator TargetTrackingDelay() //C'est une coroutine pas une simple fonction regarde l'API si besoin
    {
        yield return new WaitForSeconds(trackingDelay);     //on démarre le timer trackingDelay
        tracking = true;                                    //quand il arrive a zéro le missile peut suivre la cible
        speed = speed * 3;                                  //et sa vitesse augmente parce que c'est stylé

    }

    IEnumerator AutoDestruction()   //pour ne pas encombrer la scene d'objets inutiles si le missile n'as pas touché sa cible au bout de 10 secondes il s'autodétruit
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
