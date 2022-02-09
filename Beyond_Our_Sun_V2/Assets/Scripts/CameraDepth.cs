using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDepth : MonoBehaviour
{

    public float depthCam;

    // Start is called before the first frame update
    void Start()
    {
        // Set this camera to render after the main camera
        //cam.depth = Camera.main.depth + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
