using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSurface : MonoBehaviour
{
    public Transform ContainerControlPoints;

    //This structure will containe all the position of the Control Point for a Surface
    public List<Vector3> ControlPoint_Poss;

    private List<List<Vector3>> ControlPointsSectionsContainer;
    //********************************

    //This structure will containe all the vertices inside the mashes
    public List<Vector3> SurfacePointPositions;

    Mesh mesh; 

    private Vector3[] vertices;

    private int[] triangles;

    // Number of point in V direction
    public int Num_in_V_Direction= 10;

    // Number of point in u direction
    public int Num_in_U_Direction = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update()
    {
        generateControlPointsList();
        divideControlPointsInSection();

        calculateBezierSurface();
        MakeMesh();
    }
    
    private void generateControlPointsList()
    {
        ControlPoint_Poss = new List<Vector3>();
        SurfacePointPositions = new List<Vector3>();

        if(ContainerControlPoints.childCount!=0)
        {
            for(int i=0; i< ContainerControlPoints.childCount; i++){
                ControlPoint_Poss.Add(ContainerControlPoints.GetChild(i).position);
            }
        }
    }

    private void divideControlPointsInSection()
    {
        if(ControlPoint_Poss.Count !=0)
        {
            ControlPointsSectionsContainer = new List<List<Vector3>>();
            int square = (int)Math.Sqrt(ContainerControlPoints.childCount);
            
            for(int i=0; i< square; i++)
            {
                ControlPointsSectionsContainer.Add(new List<Vector3>(ControlPoint_Poss.GetRange(i*square, square)));
            }
        }
    }


    private void calculateBezierSurface()
    {
        if(ControlPoint_Poss.Count != 0)
        {
            SurfacePointPositions = new List<Vector3>();
            for (int i = 0; i < Num_in_U_Direction; i++)
            {
                for (int j = 0; j < Num_in_V_Direction; j++)
                {
                    float u = i / (float)(Num_in_U_Direction - 1);
                    float v = j / (float)(Num_in_V_Direction - 1);

                    SurfacePointPositions.Add(CalculateSurface(u, v));
                }
            }
        }
    }

    private Vector3 CalculateSurface(float u, float v)
    {
        List<Vector3> Allpointspos = new List<Vector3>();
        for(int i=0; i< ControlPointsSectionsContainer.Count; i++)
        {
            Allpointspos.Add(CalculateBezierCurve(u, ControlPointsSectionsContainer[i]));
        }
       
        return CalculateBezierCurve(v, Allpointspos);
    }


    private Vector3 CalculateBezierCurve(float t, List<Vector3> PointPos)
    {
        int Counter = 0;
        Vector3 Resultt = new Vector3(0f, 0f, 0f);
        for (int j = 0; j < PointPos.Count; j++)
        {
            float RR = 1;
            if (Counter != 0)
            {
                for (int i = 1; i <= Counter; i++)
                {
                    RR *= (PointPos.Count - 1) - (Counter - i);
                    RR /= i;
                }
            }
            Resultt = Resultt + RR * (float)Math.Pow((1 - t), (PointPos.Count - 1 - Counter)) * (float)Math.Pow(t, Counter) * PointPos[Counter];

            Counter = Counter + 1;
        }
        return Resultt;
    }

    public void MakeMesh() 
    {
        MakeProceduralGrid3();
        UpdateMesh();
    }

    public void MakeProceduralGrid3()
    {
        vertices = new Vector3[Num_in_V_Direction * Num_in_U_Direction];
        triangles = new int[(Num_in_V_Direction - 1) * 2 * 3 * (Num_in_U_Direction - 1)];

        for (int i = 0; i < SurfacePointPositions.Count; i++)
        {
            vertices[i] = SurfacePointPositions[i];
        }


        for (int j = 0; j < Num_in_U_Direction - 1; j++)
        {
            for (int i = 0; i < Num_in_V_Direction - 1; i++)
            {

                triangles[i * 3 + j * (Num_in_V_Direction - 1) * 2 * 3] = i + j * Num_in_V_Direction;
                triangles[i * 3 + 1 + j * (Num_in_V_Direction - 1) * 2 * 3] = i + 1 + j * Num_in_V_Direction;
                triangles[i * 3 + 2 + j * (Num_in_V_Direction - 1) * 2 * 3] = i + Num_in_V_Direction + j * Num_in_V_Direction;

            }

            for (int i = 0; i < (Num_in_V_Direction - 1); i++)
            {
                triangles[((Num_in_V_Direction - 2) * 3 + 3) + i * 3 + j * (Num_in_V_Direction - 1) * 2 * 3] = i + Num_in_V_Direction + 1 + j * Num_in_V_Direction;
                triangles[((Num_in_V_Direction - 2) * 3 + 3) + i * 3 + 1 + j * (Num_in_V_Direction - 1) * 2 * 3] = i + Num_in_V_Direction + j * Num_in_V_Direction;
                triangles[((Num_in_V_Direction - 2) * 3 + 3) + i * 3 + 2 + j * (Num_in_V_Direction - 1) * 2 * 3] = i + 1 + j * Num_in_V_Direction;

            }
        }
    }

    public void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
         
        this.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}