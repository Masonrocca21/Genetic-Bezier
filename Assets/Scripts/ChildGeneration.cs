using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject generateChildButton;
    public GameObject saveChildButton;
    public GameObject randomButton;

    public TMP_Dropdown parent1Choice;
    public TMP_Dropdown parent2Choice;


    public InputField childName;
    private Animation anim_CutPosition1;
    private Animation anim_CutPosition2;
    private Animation anim_ToggleMutation;

    private Animation anim_generateChildUp;
    private Animation anim_randomUp;
    private Animation anim_saveChildIN;

    private GameObject errorText1;
    private GameObject errorText2;

    private int CutPosition1=24;
    private int CutPosition2=24;
    private bool cutFromTheUser = false;

    int controlPoints_number=25;

    int crossover = 0;
    bool mutation = true;

    public Camera myCamera;

    private void Start() {
        anim_CutPosition1= CutPosition1_InputField.GetComponent<Animation>();
        anim_CutPosition2= CutPosition2_InputField.GetComponent<Animation>();
        anim_ToggleMutation= toggleMutation.GetComponent<Animation>();
        anim_generateChildUp = generateChildButton.GetComponent<Animation>();
        anim_saveChildIN = saveChildButton.GetComponent<Animation>();
        anim_randomUp = randomButton.GetComponent<Animation>(); 



        errorText1 = GameObject.FindGameObjectWithTag("CutPosition1Error");
        errorText1.SetActive(false);
        errorText2 = GameObject.FindGameObjectWithTag("CutPosition2Error");
        errorText2.SetActive(false);
    }

    public void StartGenetics()
    {
        Mating = new GeneticAlgorithms();

        Debug.Log("The Generation is starting");
        
        Parent1 = generateControlPointsList(ControlPointSurface_uno);
        Parent2 = generateControlPointsList(ControlPointSurface_due);

        switch(crossover)
        {
            //Single cut
            case 0:
            {
                Mating.CrossoverSingle(Parent1, Parent2, cutFromTheUser, CutPosition1);
            } break;

            //Double cut
            case 1:
            {
                Mating.CrossoverDouble(Parent1, Parent2, cutFromTheUser, CutPosition1, CutPosition2);
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
        
        Child= new List<Vector3>();
        Child = Mating.GetChild();
        ControlPointsSpawner();

        Debug.Log("Generation Ended"); 
    }

    /* Method that  after the process of creation is terminated, make spawn
    the Child surface*/
    private void ControlPointsSpawner(){
        int limit = Child.Count;
        Debug.Log(limit);

        for(int i=0 ; i < limit; i++)
        {
            GameObject cp_surface1= Instantiate(controlPoint, Child[i], Quaternion.identity, ControlPointSurface_child.transform);
    
            ControlPoints cpScript = cp_surface1.GetComponent<ControlPoints>();
            cpScript.myCamera = myCamera;
        }
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

    public void anim_saveChild()
    {
        anim_randomUp.Play("randomUp");
        anim_generateChildUp.Play("generationUp");
        anim_saveChildIN.Play("saveChildIN");
    }

    public void anim_saveChildOut()
    {
        anim_randomUp.Play("randomDown");
        anim_generateChildUp.Play("generationDown");
        anim_saveChildIN.Play("saveChildOut");
    }

    public void saveChild()
    {   
        string fileName = childName.text;
        string child;

        if(!Directory.Exists("./SavedChild")){
            Directory.CreateDirectory("./SavedChild");
        }

        if(String.Equals(fileName,""))
        {
            int number = Directory.GetFiles("./SavedChild/", "*").Length;
            ChildContainer figlio= new ChildContainer(Child.ToArray());
            child = JsonUtility.ToJson(figlio, true);
            System.IO.File.WriteAllText("./SavedChild/child"+(number+1)+".json", child);

            parent1Choice.options.Add(new TMP_Dropdown.OptionData("child"+(number+1), null));
            parent2Choice.options.Add(new TMP_Dropdown.OptionData("child"+(number+1), null));
        }
        else{
            ChildContainer figlio= new ChildContainer(Child.ToArray());
            child = JsonUtility.ToJson(figlio, true);
            System.IO.File.WriteAllText("./SavedChild/"+fileName+".json", child);

            parent1Choice.options.Add(new TMP_Dropdown.OptionData(fileName, null));
            parent2Choice.options.Add(new TMP_Dropdown.OptionData(fileName, null));
        }
        parent1Choice.RefreshShownValue();
        parent2Choice.RefreshShownValue();
    }

    [Serializable]
    public class ChildContainer{
        public Vector3[] CP_surface;

        public ChildContainer(Vector3[] child)
        {
            CP_surface = child;
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
    }

    /* Method connceted to a toggle that the user use to select
    if we do the mutaion or not */
    public void mutation_type(bool tmp_mutatition)
    {
        mutation = tmp_mutatition;
    }

    public void CPnumber(string cpNumber)
    {
        if(Mathf.Sqrt(int.Parse(cpNumber)) % 1 ==0 && int.Parse(cpNumber) <=144)
        {
            controlPoints_number = int.Parse(cpNumber);
            errorText1.SetActive(false);
            errorText2.SetActive(false);
        }
    }

    /* Metodo che recupare la posizione del taglio uno dall'input field */
    public void cutPosition1(string cut)
    {
        if(!string.Equals(cut, "Random"))
        {
            int taglio = int.Parse(cut);
            if(0 < taglio && taglio < CutPosition2 && CutPosition2 < controlPoints_number){
                CutPosition1 = taglio;
                errorText1.SetActive(false);
                cutFromTheUser = true;
            }
            else
            {
                CutPosition1 = taglio;
                errorText1.SetActive(true);
                cutFromTheUser = false;
            }
        }
    }

    /* Metodo che recupare la posizione del taglio 2 dall'input field
    se è selezionata l'opzione doppio taglio */
    public void cutPosition2(string cut)
    {
        if(!string.Equals(cut, "Random"))
        {
            int taglio = int.Parse(cut);
            if(0 < CutPosition1 && CutPosition1 < taglio && taglio < controlPoints_number)
            {
                CutPosition2 = taglio;
                errorText2.SetActive(false);
                errorText1.SetActive(false);
                cutFromTheUser = true;
            }
            else
            {
                CutPosition2 = taglio;
                errorText2.SetActive(true);
                cutFromTheUser = false;
            }
        }
    }

    /* Metodo che resetta gli Input field a Random */
    public void Randomization()
    {
        cutFromTheUser = false;
        errorText1.SetActive(false);
        errorText2.SetActive(false);

        CutPosition1 = controlPoints_number-1;
        CutPosition2 = controlPoints_number-1;

        CutPosition1_InputField.GetComponent<TMP_InputField>().text="Random";
        CutPosition2_InputField.GetComponent<TMP_InputField>().text="Random";
    }
}
