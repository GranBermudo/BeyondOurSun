using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public int ennemisAbattus, totalEnnemis;
    public Text objectifText;
    public GameObject Fin_Mission;
    public GameObject LifeCanvas;
    

    // Start is called before the first frame update
    void Start()
    {
        objectifText.text = "Abattre " + ennemisAbattus + "/" + totalEnnemis + " ennemis";
        
    }

    // Update is called once per frame
    public void EnnemisDetruit()
    {
        ennemisAbattus += 1;


        objectifText.text = "Abattre " + ennemisAbattus + "/" + totalEnnemis + " ennemis";

        if(ennemisAbattus == 5)
        {
            Fin_Mission.SetActive(true);
            LifeCanvas.SetActive(false);
            Time.timeScale = 0f;
        }
        


    }

   
    
}
