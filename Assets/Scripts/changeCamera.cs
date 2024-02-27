using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cameraParent1;
    public GameObject cameraParent2;
    public GameObject cameraChild;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            CameraParent1();
        }

        if(Input.GetKeyDown("2"))
        {
            CameraParent2();
        }
    }

    private void CameraParent2()
    {
        cameraParent1.SetActive(true);
        cameraParent2.SetActive(false);
    }

    private void CameraParent1()
    {
        cameraParent2.SetActive(true);
        cameraParent1.SetActive(false);
    }
}
