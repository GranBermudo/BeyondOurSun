using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTargetList : MonoBehaviour
{
    private WeaponSystem playerShipBehaviour;
    private bool spotted = false;   //bool pour envoyer une seule fois l'objet dans la liste des cibles

    private void Awake()
    {
        playerShipBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>(); //chopper une ref a la liste des cibles, y a surement une alternative au FindGameobject a voir avec Bruno
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, playerShipBehaviour.transform.position) <= playerShipBehaviour.detectionRange && spotted == false)
        {
            playerShipBehaviour.TargetsInSight.Add(this.gameObject);    //si le joueur est assez proche l'objet entre dans la liste des cibles du joueur
            spotted = true;
        }
        else if (Vector3.Distance(transform.position, playerShipBehaviour.transform.position) > playerShipBehaviour.detectionRange)
        {
            playerShipBehaviour.TargetsInSight.Remove(this.gameObject);     //et si il est trop loin il s'en retire
            spotted = false;
        }
    }
}
