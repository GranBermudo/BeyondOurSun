using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float PV;
    public UnityEvent eventOnDeath;

    public void TakeDammage(float dammage)

	{
        PV -= dammage;

        if (PV <= 0)
		{
            eventOnDeath.Invoke();
		}
	}



}
