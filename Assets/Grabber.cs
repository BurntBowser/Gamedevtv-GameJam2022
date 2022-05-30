using System;
using UnityEngine;

public class Grabber : MonoBehaviour 
{

    public GameObject selectedObject;
    public bool isPickedUp = false;
    RaycastHit hit;

    private void Update() 
    {
        if (isPickedUp == true) 
        {            
            selectedObject = GameObject.FindGameObjectWithTag("placing");
            
            if(selectedObject == null) 
            {
               return;
            } 
            else if(Input.GetMouseButton(0))
            {
                selectedObject.GetComponent<BoxCollider>().enabled = false;
                RaycastHit hit = CastRay();
                if (hit.collider !=null && hit.collider.tag == "Buildable")
                {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, hit.point.y, worldPosition.z);

                isPickedUp = false;
                selectedObject.GetComponent<BoxCollider>().enabled = true;
                selectedObject = null;
                Cursor.visible = true;
                
                }
            
            }
            else
            {
                return;
            }
        }

        if(selectedObject != null && isPickedUp == true) 
        {
            hit = CastRay();
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x,hit.point.y+0.25f, worldPosition.z);;
            }
            
        }

    

    private RaycastHit CastRay() 
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}

