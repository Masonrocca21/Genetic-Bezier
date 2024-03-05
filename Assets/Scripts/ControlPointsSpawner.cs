using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointsSpawner : MonoBehaviour
{
    private int controlPointsNumber = 25; 

    //Reference to the prefab that we will istantiate as Control Point of the surface
    public GameObject controlPoint;

    /* Reference to the 2 partent Game Object that will contain
    the control points for the two parent surface */
    public GameObject cp_Surface1;
    public GameObject cp_Surface2;

    public Camera myCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnControlPoints();
    }

    /** Method that spaw the control points of the two parent in the scene
    using a grid disposition. The number of control points must a perfect square
    and can be given as a input by the user. **/
    private void spawnControlPoints()
    {
        int limit = (int)Math.Sqrt(controlPointsNumber);

        for(int i=0; i< limit; i++)
        {
            for(int j=limit-1; j>=0; j--)
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

    /* Method connected to an input field in the UI, that the user can use
    to give the number of control points.
    This number mast be a perfect square and less than 144.
    After the input is given by the User, we start to make spanw the control Points. */
    public void readStringInput(string numberOfControlPoints)
    {
        if(Mathf.Sqrt(int.Parse(numberOfControlPoints)) % 1 ==0 && int.Parse(numberOfControlPoints) <=144)
        {
            controlPointsNumber = int.Parse(numberOfControlPoints);
        }
        spawnControlPoints();
    }
}
