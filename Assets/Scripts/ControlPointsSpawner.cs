using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointsSpawner : MonoBehaviour
{
    public string controlPointsNumber; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void readStringInput(string numberOfControlPoints)
    {
        controlPointsNumber = numberOfControlPoints;
        Debug.Log(controlPointsNumber);
    }
}
