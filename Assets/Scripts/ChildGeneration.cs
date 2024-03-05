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

    int crossover = 0;
    bool mutation = true;

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
        Mating = new GeneticAlgorithms();

        Debug.Log("The Generation is starting0");
        
        Parent1 = generateControlPointsList(ControlPointSurface_uno);
        Parent2 = generateControlPointsList(ControlPointSurface_due);

        switch(crossover)
        {
            //Single cut
            case 0:
            {
                Mating.CrossoverSingle(Parent1, Parent2);
            } break;

            //Double cut
            case 1:
            {
                Mating.CrossoverDouble(Parent1, Parent2);
            } break;

            //Uniform Cut
            case 2:
            {
                Mating.CrossoverUniform(Parent1, Parent2);
            } break;
        }

        if(mutation)
        {
            Mating.Mutation();
        }
        
        Child = Mating.GetChild();
        ControlPointsSpawner();

        Debug.Log("Generation finita"); 
    }

    private List<Vector3> generateControlPointsList(GameObject parent)
    {
        List<Vector3> ControlPoint_Poss = new List<Vector3>();

        for(int i=0; i< parent.transform.childCount; i++){
            ControlPoint_Poss.Add(parent.transform.GetChild(i).localPosition);
        }

        return ControlPoint_Poss;
    }

    private void ControlPointsSpawner(){
        int limit = (int)Math.Sqrt(Child.Count);

        /* for(int i=0; i< limit; i++)
        {
            for(int j=limit-1; j>=0; j--)
            {
                Vector3 delta = new Vector3(i,0, 3*j);
                GameObject cp_surface1= Instantiate(controlPoint, ControlPointSurface_child.transform.position+delta, Quaternion.identity, ControlPointSurface_child.transform);
                
                ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
                cpScript.myCamera = myCamera;
            }
        } */

        Debug.Log("CP figlio: "+Child.Count);

        for(int i=0 ; i < limit*limit; i++)
        {
            GameObject cp_surface1= Instantiate(controlPoint, Child[i], Quaternion.identity, ControlPointSurface_child.transform);
    
            ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
            cpScript.myCamera = myCamera;
        }
    }

    public void crossoverType(Int32 type)
    {
        crossover = type;
        Debug.Log("Crossover type "+crossover);
    }

    public void mutation_type(bool tmp_mutatition)
    {
        mutation = tmp_mutatition;
        Debug.Log("Mutation: "+mutation);
    }
}
