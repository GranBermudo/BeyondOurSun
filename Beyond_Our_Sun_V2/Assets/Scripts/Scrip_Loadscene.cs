using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scrip_Loadscene : MonoBehaviour
{

    private string[] scenePaths;
    public Text ObjectifText;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        //ObjectifText.text = "Abbatre " + EnnemisAbbatus + "/" + TotalEnnemis + " ennemis";

        ObjectifText.text = "Jouer ";
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
        {
            Debug.Log("Scene_Mission01 loading: " + scenePaths[0]);
            SceneManager.LoadScene(scenePaths[0], LoadSceneMode.Single);
        }
    }
}

