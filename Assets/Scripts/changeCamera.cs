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
        CameraParent1();
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

        if(Input.GetKeyDown("3"))
        {
            CameraChild();
        }
    }

    private void CameraChild()
    {
        cameraChild.SetActive(true);
        cameraParent1.SetActive(false);
        cameraParent2.SetActive(false);
    }

    private void CameraParent2()
    {
        cameraParent1.SetActive(false);
        cameraParent2.SetActive(true);
        cameraChild.SetActive(false);
    }

    private void CameraParent1()
    {
        cameraParent2.SetActive(false);
        cameraParent1.SetActive(true);
        cameraChild.SetActive(false);
    }
}
