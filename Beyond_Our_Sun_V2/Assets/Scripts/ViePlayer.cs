using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViePlayer : MonoBehaviour
{
    //public float PV;
    //public UnityEvent eventOnDeath;

    public float playerHealth;
    [SerializeField] private Image[] hearts;
	

	/* public void TakeDammage(float dammage)

	 {
		 PV -= dammage;
	/////////////////////////////////////////////////// SYSTEME DE VIE DE BRUNO CE QUI EST EN VERT////////////////////////////
		 if (PV <= 0)
		 {
			 eventOnDeath.Invoke();
		 }
	 }*/

	private void Start()
	{
		UpdateHealth();
	}

	public void UpdateHealth()
	{
		if (playerHealth <= 0)
		{
			//Restart the game
			//Restart the player
		}
	
        for (int i= 0; i < hearts.Length; i++)
		{
            if(i< playerHealth)
				{
                hearts[i].color = Color.red;
				}
			else
			{
                hearts[i].color = Color.black;

            }
		}
	}
	
	public void TakeDammage (float dammage)
	{
		playerHealth -= dammage;
		UpdateHealth();
	}



}