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
    public GeneticAlgorithms Mating;

    public GameObject CutPosition1_InputField;
    public GameObject CutPosition2_InputField;
    
    public GameObject toggleMutation;

    private Animation anim_CutPosition1;
    private Animation anim_CutPosition2;
    private Animation anim_ToggleMutation;

    int crossover = 0;
    bool mutation = true;

    public Camera myCamera;

    private void Start() {
        anim_CutPosition1= CutPosition1_InputField.GetComponent<Animation>();
        anim_CutPosition2= CutPosition2_InputField.GetComponent<Animation>();
        anim_ToggleMutation= toggleMutation.GetComponent<Animation>();
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

        Debug.Log("Generation Ended"); 
    }

    /* Method that given the Containet of a series of control Points return 
    a List of Vector3 that contain the positions of this control Points */
    private List<Vector3> generateControlPointsList(GameObject container)
    {
        List<Vector3> ControlPoint_Poss = new List<Vector3>();

        for(int i=0; i< container.transform.childCount; i++){
            ControlPoint_Poss.Add(container.transform.GetChild(i).localPosition);
        }

        return ControlPoint_Poss;
    }

    /* Method that  after the process of creation is terminated, make spawn
    the Child surface*/
    private void ControlPointsSpawner(){
        int limit = (int)Math.Sqrt(Child.Count);

        for(int i=0 ; i < limit*limit; i++)
        {
            GameObject cp_surface1= Instantiate(controlPoint, Child[i], Quaternion.identity, ControlPointSurface_child.transform);
    
            ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
            cpScript.myCamera = myCamera;
        }
    }

    /* Method connceted to a scroll menu, that the user use
    to selecet the type of crossover that we will use */
    public void crossoverType(Int32 type)
    {
        switch(type){
            //Single cut
            case 0:
            {
                if(crossover ==1)
                {
                    anim_CutPosition2.Play("cutPosition2Reverse");
                    anim_ToggleMutation.Play("DownToggleButtonReverse");
                }
                else if(crossover==2){
                    anim_ToggleMutation.Play("DownToggleButtonMid");
                    anim_CutPosition1.Play();
                }
            } break;

            //Double cut
            case 1:
            {
                anim_CutPosition1.Play();
                anim_CutPosition2.Play();
                anim_ToggleMutation.Play("DownToggleButton");
            } break;

            //Uniform Cut
            case 2:
            {
                anim_CutPosition1.Play("cutPosition1Reverse");
                anim_CutPosition2.Play("cutPosition2Reverse");
                anim_ToggleMutation.Play("DownToggleButtonReverseTotal");
            } break;
        }

        crossover = type;
        Debug.Log("Crossover type "+crossover);
    }

    /* Method connceted to a toggle that the user use to select
    if we do the mutaion or not */
    public void mutation_type(bool tmp_mutatition)
    {
        mutation = tmp_mutatition;
        Debug.Log("Mutation: "+mutation);
    }
}
