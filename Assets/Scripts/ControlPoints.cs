using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoints : MonoBehaviour
{
    Vector3 Dist;
    float posX;
    float posY;

    public Camera myCamera;

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
    }
}
