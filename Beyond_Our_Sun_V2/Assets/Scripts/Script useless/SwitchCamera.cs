using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    public GameObject[] Cameras;

    int currentCam;

    public bool buttonpress;

    public GameObject MainCameraTps;
    public GameObject CameraRetroviseur;
    public GameObject CameraFps;

    // Start is called before the first frame update
    void Start()
    {
        currentCam = 0;
        setCam(currentCam);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setCam(int idx)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == idx)
            {
                Cameras[i].SetActive(true);
            }
            else
            {
                Cameras[i].SetActive(false);
            }
        }
    }

    public void toggleCam()
    {
        currentCam++;
        if (currentCam > Cameras.Length - 1)
            currentCam = 0;
        setCam(currentCam);

        if (Input.GetButtonDown("Xbutton"))        //changer la camera en appuyant sur le bouton B
        {
            buttonpress = true;
            Debug.Log("buttonXpress");

            CameraRetroviseur.SetActive(true);
            MainCameraTps.SetActive(false);
            //currentCam = 0; //augmenter la valeur de la cam a 1 ou appeler le game object camera
        }
        //else (Input.GetButtonDown("Xbutton"))

        {
            buttonpress = false;
            currentCam = 0;
            CameraRetroviseur.SetActive(false);
            MainCameraTps.SetActive(true);
            
        }

        if (Input.GetButtonDown("RightSitckButton")) //changer la camera en appuyant sur le stick droit
        {
            buttonpress = true;

            CameraFps.SetActive(true);
            MainCameraTps.SetActive(false);
            // currentCam = 2;
        }

        //else (Input.GetButtonDown("RightSitckButton"))    //pourquoi il demande un ; ici ?


        {
            buttonpress = false;

            CameraFps.SetActive(false);
            MainCameraTps.SetActive(true);
            currentCam = 0;
        }
    }
}