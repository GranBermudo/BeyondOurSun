using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStats : MonoBehaviour
{
    public PropellerShipBehaviour PSBscript;

    public Text SpeedText;

    // Update is called once per frame
    void Update()
    {
        SpeedText.text = "Speed = " + PSBscript.Speed.ToString();
    }
}
