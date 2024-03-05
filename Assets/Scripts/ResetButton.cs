using UnityEngine;

public class ResetButton : MonoBehaviour
{

    public GameObject controlPoints_Surface1;
    public GameObject controlPoints_Surface2;
    public GameObject controlPoints_Child;

    private void Start() {
        
    }

    public void Reset()
    {
        deleteControlPoints(controlPoints_Surface1);
        deleteControlPoints(controlPoints_Surface2);
        //deleteControlPoints(controlPoints_Child);
    }


    private void deleteControlPoints(GameObject Container)
    {
        if(Container.transform.childCount!=0)
        {
            for(int i=0; i< Container.transform.childCount; i++)
            {
                Destroy(Container.transform.GetChild(i).gameObject);
            }
        }
    }
}
