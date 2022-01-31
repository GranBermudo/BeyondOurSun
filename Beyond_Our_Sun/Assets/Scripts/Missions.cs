using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public int ennemisAbbatus, totalEnnemis;
    public Text objectifText;

    // Start is called before the first frame update
    void Start()
    {
        objectifText.text = "Abbatre " + ennemisAbbatus + "/" + totalEnnemis + " ennemis";
    }

    // Update is called once per frame
    public void EnnemisDetruit()
    {
        ennemisAbbatus += 1;


        objectifText.text = "Abbatre " + ennemisAbbatus + "/" + totalEnnemis + " ennemis";



    }
}
