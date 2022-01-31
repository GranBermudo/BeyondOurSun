using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //necessaire de rajouter ça si tu veux toucher a l'UI

public class UiStats : MonoBehaviour
{
    public PropellerShipBehaviour PSBscript;

    public Text SpeedText;

    // Update is called once per frame
    void Update()
    {
        SpeedText.text = "Speed = " + PSBscript.Speed.ToString(); // juste pour indiquer la vitese actuelle sur un élément d'UI a l'écran
    }
}
