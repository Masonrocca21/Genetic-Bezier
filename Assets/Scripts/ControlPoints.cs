using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    /* Set of variable to take note about the movement of a Control point */
    Vector3 Dist;
    float posX;
    float posY;

    private Vector3 Inizial_pos;
    public float limit = 5;

    /* Reference to the camera of the scene */
    public Camera myCamera;

    public void Start(){
        Inizial_pos = this.transform.position;
    }

    public void setMyCamera(Camera camera)
    {
        myCamera = camera;
    }

    private void OnMouseDown()
    {
        Dist = myCamera.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - Dist.x;
        posY = Input.mousePosition.y - Dist.y;

    }

    private void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, Dist.z);
        Vector3 worldPos = myCamera.ScreenToWorldPoint(curPos);
        
        Check_distance(worldPos);
        //Debug.Log("Muovi punto di controllo");
    }

    private void Check_distance(Vector3 new_pos)
    {
        Vector2 Aux = new Vector2((new_pos - Inizial_pos).x, (new_pos - Inizial_pos).z);   
        if(Aux.magnitude > limit){

            Vector2 Inizial_pos2 = new Vector2(Inizial_pos.x, Inizial_pos.z);
            Aux = Inizial_pos2 + Aux.normalized*limit; 
            this.transform.SetPositionAndRotation(new Vector3 (Aux.x, new_pos.y, Aux.y), Quaternion.identity); 
        }
        else {
            transform.position = new_pos;
        }
    }
}
