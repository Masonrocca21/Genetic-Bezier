using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public TMP_InputField CutPosition1_InputField;
    public TMP_InputField CutPosition2_InputField;
    public Toggle toggleMutation;
    public Button generateChildButton;
    public Button saveChildButton;
    public Button randomButton;

    private Animation anim_CutPosition1;
    private Animation anim_CutPosition2;
    private Animation anim_ToggleMutation;
    private Animation anim_generateChildUp;
    private Animation anim_randomUp;
    private Animation anim_saveChildIN;
    private GameObject errorText1;
    private GameObject errorText2;

    bool saveButtonisIN = false;
    int crossover=0;
    public Camera myCamera;

    private void Start() {
        anim_CutPosition1= CutPosition1_InputField.GetComponent<Animation>();
        anim_CutPosition2= CutPosition2_InputField.GetComponent<Animation>();
        anim_ToggleMutation= toggleMutation.GetComponent<Animation>();
        anim_generateChildUp = generateChildButton.GetComponent<Animation>();
        anim_saveChildIN = saveChildButton.GetComponent<Animation>();
        anim_randomUp = randomButton.GetComponent<Animation>(); 

        errorText1 = GameObject.FindGameObjectWithTag("CutPosition1Error");
        errorText2 = GameObject.FindGameObjectWithTag("CutPosition2Error");
    }

    public void anim_saveChild()
    {
        if(!saveButtonisIN)
        {
            anim_randomUp.Play("randomUp");
            anim_generateChildUp.Play("generationUp");
            anim_saveChildIN.Play("saveChildIN");
            saveButtonisIN = true;
        }
    }

    public void anim_saveChildOut()
    {
        if(saveButtonisIN)
        {
            anim_randomUp.Play("randomDown");
            anim_generateChildUp.Play("generationDown");
            anim_saveChildIN.Play("saveChildOut");
            saveButtonisIN = false;
        }
    }

    public void anim_crossoverType(Int32 type)
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

    public void CPnumber(string cpNumber)
    {
        if(Mathf.Sqrt(int.Parse(cpNumber)) % 1 ==0 && 1 < int.Parse(cpNumber) && int.Parse(cpNumber) <= 36)
        {
            errorText1.GetComponent<TMP_Text>().enabled = false;
            errorText2.GetComponent<TMP_Text>().enabled = false;
        }
    }
}
