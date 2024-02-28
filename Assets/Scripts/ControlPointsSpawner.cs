using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointsSpawner : MonoBehaviour
{
    private int controlPointsNumber = 25; 

    //Riferimento al prefab che vogliamo istanziare
    public GameObject controlPoint;

    //I due gameObject che descrivono il parent in cui andremo ad 
    //inserire i vari control point delle due superfici
    public GameObject cp_Surface1;
    public GameObject cp_Surface2;

    //Riferimento alla camera, necessario per settare la camera nel prefab
    //che andiamo ad istanziare, altrimenti non si muove.
    public Camera myCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnControlPoints();
    }

    private void spawnControlPoints()
    {
        int limit = (int)Math.Sqrt(controlPointsNumber);

        for(int i=0; i< limit; i++)
        {
            for(int j=4; j>=0; j--)
            {
                Vector3 delta = new Vector3(i,0, 3*j);
                GameObject cp_surface1= Instantiate(controlPoint, cp_Surface1.transform.position+delta, Quaternion.identity, cp_Surface1.transform);
                GameObject cp_surface2= Instantiate(controlPoint, cp_Surface2.transform.position+delta, Quaternion.identity, cp_Surface2.transform);
                
                ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
                cpScript.myCamera = myCamera;
                
                ControlPoints cpScript_2 = cp_surface2.GetComponent<ControlPoints>();
                cpScript_2.myCamera = myCamera;

            }
        }
    }

    //Metodo che legge da input il numero di control Point che vogliamo istanziare
    public void readStringInput(string numberOfControlPoints)
    {
        if(Mathf.Sqrt(int.Parse(numberOfControlPoints)) % 1 ==0)
        {
            controlPointsNumber = int.Parse(numberOfControlPoints);
        }
        Debug.Log(controlPointsNumber);

        spawnControlPoints();
    }
}
