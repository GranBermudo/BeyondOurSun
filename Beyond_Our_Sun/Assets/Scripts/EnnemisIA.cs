using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisIA : MonoBehaviour
{

    private Transform player;
    public float distanceDeDetection;
    public float vitesse;

    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //chopper une ref a la liste des cibles, y a surement une alternative au FindGameobject a voir avec Bruno
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > distanceDeDetection) 
		{

            //patrouille
		}
        else
		{
            //poursuite
            transform.LookAt(player.position);
            transform.Translate(Vector3.forward * vitesse * Time.deltaTime); //Possibilite d'augmenter la vitesse pour ennemi kamikaze
		}
    }
}
