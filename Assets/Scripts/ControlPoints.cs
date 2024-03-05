using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    /* Set of variable to take note about the movement of a Control point */
    Vector3 Dist;
    float posX;
    float posY;

    /* Reference to the camera of the scene */
    public Camera myCamera;

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
        transform.position = worldPos;
        //Debug.Log("Muovi punto di controllo");
    }
}
