using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCam : MonoBehaviour
{

    public CinemachineVirtualCamera FrontCam;
    public GameObject Fps;
    public bool IsFpsMode;
    public bool IsBackmode;


    // Start is called before the first frame update
    void Start()
    {
        Fps.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
///////////////////////////////////////////////// APPUYER SUR LE BOUTON X ET CHANGERE LA PRIORITE DES CAMERAS ////////////////////////////////////////
        if (Input.GetButton("RightStickButton"))
        {
            IsBackmode = true;
            FrontCam.m_Priority = 7;
        }

        else 
        {
            IsBackmode = false;
            FrontCam.m_Priority = 9;
        }
       
        ///////////////////////////////////////////////// CONDITION POUR REGARDER LE RETRO EN FPS MODE //////////////////////////////////////////
        if (IsFpsMode == true && Input.GetButton("RightStickButton"))
		{

            IsBackmode = true;
            Fps.SetActive(false);
        }

        else if (IsFpsMode == true)
        {
            IsBackmode = false;
            Fps.SetActive(true);
        }


        /////////////////////////////////////////////// FLIP FLOP RIGHT STICK BUTUTON //////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetButtonDown("LeftStickButton")) // SI t'appuie sur le bouton 
        {
            IsFpsMode = !IsFpsMode;   // la condition isfpsmode s'inverse. Du coup ça veut dire que si c'est true , bah ca devient false et si c'est false ça deviant true 
            if(IsFpsMode == true)           // donc si c'est true la con de ta mere  ça active la camera
			{
                IsFpsMode = true;
                Fps.SetActive(true);
            }
            else  // donc si c'est false la con de ta mere ca desavtice la camera
            {
                IsFpsMode = false;
                Fps.SetActive(false);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
