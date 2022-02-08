using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifetime;
    public int layerNumber;
    public float degats;

    // Update is called once per frame
    void Update()
    {
        Lifetime -= Time.deltaTime;     //au bout d'un certain temps l'objet s'autodétruit pour ne pas encombrer la scene, ça pourrait être fait avec une Coroutine
        if(Lifetime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerNumber)  //si collision avec le layer indiqué (9 pour les ennemis)
        {
            other.gameObject.GetComponent<Health>().TakeDammage(degats);  //faire des dégats a rajouter
            Debug.Log(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
