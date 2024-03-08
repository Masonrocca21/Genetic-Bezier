using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSurfaceAnalysis : MonoBehaviour
{
    public GameObject ControlPoints_Parent1;
    public GameObject ControlPoints_Parent2;
    public GameObject ControlPoints_Child;
    public GameObject result;
    public GameObject GeneticsAlgorithm;

    private int Parent1_CP_number;

    // Start is called before the first frame update
    void Start()
    {
        Parent1_CP_number = GeneticsAlgorithm.GetComponent<ChildGeneration>().Mating.Parent1_CP_number;
    }

    public void getResult()
    {

    }
}
