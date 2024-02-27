using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSurface : MonoBehaviour
{
    public Transform ContainerControlPoints;

    //This structure will containe all the position of the Control Point for a Surface
    public List<Vector3> ControlPoint_Poss;

    //******************************
    private List<Vector3> controlPointsSection1;
    private List<Vector3> controlPointsSection2;
    private List<Vector3> controlPointsSection3;
    private List<Vector3> controlPointsSection4;
    private List<Vector3> controlPointsSection5;
    //********************************

    //This structure will containe all the vertices inside the mashes
    public List<Vector3> SurfacePointPositions;

    Mesh mesh; 

    Vector3[] vertices;

    int[] triangles;

    // Number of point in V direction
    public int Num_in_V_Direction= 50;

    // Number of point in u direction
    public int Num_in_U_Direction = 50;

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
    
    //
    private void generateControlPointsList()
    {
        ControlPoint_Poss = new List<Vector3>();

        for(int i=0; i< ContainerControlPoints.childCount; i++){
            ControlPoint_Poss.Add(ContainerControlPoints.GetChild(i).position);
        }
    }

    //Metodo che divide i control Points i 4 sezione da 5 nodi ognuna
    private void divideControlPointsInSection()
    {
        controlPointsSection1 = new List<Vector3>();
        controlPointsSection2 = new List<Vector3>();
        controlPointsSection3 = new List<Vector3>();
        controlPointsSection4 = new List<Vector3>();
        controlPointsSection5 = new List<Vector3>();

        for(int i=0; i<5; i++)
        {
            controlPointsSection1.Add(ControlPoint_Poss[i]);
            controlPointsSection2.Add(ControlPoint_Poss[i+5]);
            controlPointsSection3.Add(ControlPoint_Poss[i+10]);
            controlPointsSection4.Add(ControlPoint_Poss[i+15]);
            controlPointsSection5.Add(ControlPoint_Poss[i+20]);
        }
    }


    private void calculateBezierSurface()
    {
        SurfacePointPositions = new List<Vector3>();
        for (int i = 0; i < Num_in_U_Direction; i++)
        {
            for (int j = 0; j < Num_in_V_Direction; j++)
            {
                float u = i / (float)(Num_in_U_Direction - 1);
                float v = j / (float)(Num_in_V_Direction - 1);

                SurfacePointPositions.Add(CalculateSurface(u, v, ControlPoint_Poss));
            }
        }
    }

    private Vector3 CalculateSurface(float u, float v, List<Vector3> PointPos)
    {
        List<Vector3> Allpointspos = new List<Vector3>
        {
            CalculateBezierCurve(u, controlPointsSection1),
            CalculateBezierCurve(u, controlPointsSection2),
            CalculateBezierCurve(u, controlPointsSection3),
            CalculateBezierCurve(u, controlPointsSection4),
            CalculateBezierCurve(u, controlPointsSection5)
        };
       
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


    //
    public void MakeMesh() 
    {
        MakeProceduralGrid3();
        UpdateMesh();
    }

    //
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


    //
    public void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        
        this.GetComponent<MeshFilter>().sharedMesh = mesh;
    }

}