using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControlPointsSpawner : MonoBehaviour
{
    private int controlPointsNumber = 25; 

    /* Reference to the 2 partent Game Object that will contain
    the control points for the two parent surface */
    public GameObject controlPoints_Surface1;
    public GameObject controlPoints_Surface2;
    public GameObject controlPoints_Child;
    public GameObject controlPoint_prefab;

    public TMP_Dropdown parent1Choice;
    public TMP_Dropdown parent2Choice;

    public Camera myCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnControlPoints();

        initializeParentChoice();
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
                GameObject cp_surface1= Instantiate(controlPoint_prefab, 
                    controlPoints_Surface1.transform.position+delta, 
                    Quaternion.identity, controlPoints_Surface1.transform);
                GameObject cp_surface2= Instantiate(controlPoint_prefab, 
                    controlPoints_Surface2.transform.position+delta, 
                    Quaternion.identity, controlPoints_Surface2.transform);
                
                ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
                cpScript.myCamera = myCamera;
                
                ControlPoints cpScript_2 = cp_surface2.GetComponent<ControlPoints>();
                cpScript_2.myCamera = myCamera;
            }
        }
    }

    [Serializable]
    public class ChildContainer{
        public Vector3[] CP_surface;

        public ChildContainer(Vector3[] child)
        {
            CP_surface = child;
        }
    }

    public void getParent1(Int32 position)
    {
        if(Directory.Exists("./SavedChild"))
        {
            string[] savedChild= Directory.GetFiles("./SavedChild/").OrderBy(d => new FileInfo(d).CreationTime).ToArray();;

            ChildContainer parent = JsonUtility.FromJson<ChildContainer>(File.ReadAllText(savedChild[position]));
            spawnParent(parent.CP_surface, controlPoints_Surface1);
        }
    }
    
    public void getParent2(Int32 position)
    {
        if(Directory.Exists("./SavedChild"))
        {
            string[] savedChild= Directory.GetFiles("./SavedChild/").OrderBy(d => new FileInfo(d).CreationTime).ToArray();

            ChildContainer parent = JsonUtility.FromJson<ChildContainer>(File.ReadAllText(savedChild[position]));
            spawnParent(parent.CP_surface, controlPoints_Surface2);
        }
    }

    private void spawnParent(Vector3[] parent, GameObject id)
    {   
        int limit = parent.Length;

        for(int i=0; i< limit; i++)
        {
            id.transform.GetChild(i).transform.position = id.transform.position + parent[i];
        }
    }

    public void initializeParentChoice()
    {
        if(Directory.Exists("./SavedChild"))
        {
            string[] savedChild= Directory.GetFiles("./SavedChild/").OrderBy(d => new FileInfo(d).CreationTime).ToArray();
            int n_child = savedChild.Length;

            foreach (string tmp in savedChild)
            {
                parent1Choice.options.Add(new TMP_Dropdown.OptionData(tmp.Substring(13, tmp.Length-18), null));
                parent2Choice.options.Add(new TMP_Dropdown.OptionData(tmp.Substring(13, tmp.Length-18), null));
            }

            parent1Choice.RefreshShownValue();
            parent2Choice.RefreshShownValue();
        }
    }

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
                GameObject cp_surface1= Instantiate(controlPoint_prefab, 
                    controlPoints_Surface1.transform.position+delta, 
                    Quaternion.identity, controlPoints_Surface1.transform);
                GameObject cp_surface2= Instantiate(controlPoint_prefab, 
                    controlPoints_Surface2.transform.position+delta, 
                        Quaternion.identity, controlPoints_Surface2.transform);
                
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
    public void deleteControlPoints(GameObject Container)
    {
        if(Container.transform.childCount!=0)
        {
            for(int i=0; i< Container.transform.childCount; i++)
            {
                Destroy(Container.transform.GetChild(i).gameObject);
            }
        }
    }

    /* Method connected to an input field in the UI, that the user can use
    to give the number of control points.
    This number mast be a perfect square and less than 144.
    After the input is given by the User, we start to make spanw the control Points. */
    public void readStringInput(string numberOfControlPoints)
    {
        if(Mathf.Sqrt(int.Parse(numberOfControlPoints)) % 1 ==0 && 1 < int.Parse(numberOfControlPoints) && int.Parse(numberOfControlPoints) <= 36)
        {
            controlPointsNumber = int.Parse(numberOfControlPoints);
        }
        spawnControlPoints();
    }
}
