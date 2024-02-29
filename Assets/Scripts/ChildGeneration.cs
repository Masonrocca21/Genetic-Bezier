using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildGeneration : MonoBehaviour
{
    public GameObject ControlPointSurface_uno;
    public GameObject ControlPointSurface_due;
    public GameObject ControlPointSurface_child;
    public GameObject controlPoint;
    private List<Vector3> Child;
    private List<Vector3> Parent1;
    private List<Vector3> Parent2;
    private GeneticAlgorithms Mating;

    public Camera myCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGenetics()
    {
        Parent1 = generateControlPointsList(ControlPointSurface_uno);
        Parent2 = generateControlPointsList(ControlPointSurface_due);

        //Aggiungere metodo per crossover leggendo input da scroll menu
        //Aggiungere mutazione
        
        Child = Mating.GetChild();
        ControlPointsSpawner();
    }

    private List<Vector3> generateControlPointsList(GameObject parent)
    {
        List<Vector3> ControlPoint_Poss = new List<Vector3>();

        for(int i=0; i< parent.transform.childCount; i++){
            ControlPoint_Poss.Add(parent.transform.GetChild(i).position);
        }

        return ControlPoint_Poss;
    }

    private void ControlPointsSpawner(){
        int limit = (int)Math.Sqrt(Child.Count);

        for(int i=0; i< limit; i++)
        {
            for(int j=4; j>=0; j--)
            {
                Vector3 delta = new Vector3(i,0, 3*j);
                GameObject cp_surface1= Instantiate(controlPoint, ControlPointSurface_child.transform.position+delta, Quaternion.identity, ControlPointSurface_child.transform);
                
                ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
                cpScript.myCamera = myCamera;

            }
        }
    }
}
