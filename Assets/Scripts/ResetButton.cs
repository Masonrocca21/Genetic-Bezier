using System;
using UnityEngine;

public class ResetButton : MonoBehaviour
{

    public GameObject controlPoints_Surface1;
    public GameObject controlPoints_Surface2;
    public GameObject controlPoints_Child;
    public GameObject controlPoint;
    public Camera myCamera;

    /* Method connected to the Reset Button in the scene.
    This method is used to Delete all the surface in the Scene, even the 
    Child if it's in. And then reset to two parent prior any modification
    that is been made on them. */
    public void Reset()
    {
        int limit = (int)Math.Sqrt(controlPoints_Surface1.transform.childCount);

        deleteControlPoints(controlPoints_Surface1);
        deleteControlPoints(controlPoints_Surface2);
        
        if(controlPoints_Child.transform.childCount !=0 )
        {
            deleteControlPoints(controlPoints_Child);
        }

        for(int i=0; i< limit; i++)
        {
            for(int j=limit-1; j>=0; j--)
            {
                Vector3 delta = new Vector3(i,0, 3*j);
                GameObject cp_surface1= Instantiate(controlPoint, controlPoints_Surface1.transform.position+delta, Quaternion.identity, controlPoints_Surface1.transform);
                GameObject cp_surface2= Instantiate(controlPoint, controlPoints_Surface2.transform.position+delta, Quaternion.identity, controlPoints_Surface2.transform);
                
                ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
                cpScript.myCamera = myCamera;
                
                ControlPoints cpScript_2 = cp_surface2.GetComponent<ControlPoints>();
                cpScript_2.myCamera = myCamera;
            }
        }
    }

    /* Method that completely delete all the surface in the Scene,
    the two Parent and the Child if it's in.
    Method connected to the input field in the scene, that the user use to
    set the number of control point */
    public void TotalReset()
    {
        deleteControlPoints(controlPoints_Surface1);
        deleteControlPoints(controlPoints_Surface2);


        if(controlPoints_Child.transform.childCount !=0 )
        {
            deleteControlPoints(controlPoints_Child);
        }
    }

    /* Method that given the GameObject that contain all the control Point of a surface,
    delete all of them */
    private void deleteControlPoints(GameObject Container)
    {
        if(Container.transform.childCount!=0)
        {
            for(int i=0; i< Container.transform.childCount; i++)
            {
                Destroy(Container.transform.GetChild(i).gameObject);
            }
        }
    }
}
