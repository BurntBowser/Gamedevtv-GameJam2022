using System;
using UnityEngine;

public class Grabber : MonoBehaviour {

    private GameObject selectedObject;
    public bool isPickedUp = false;


    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if(selectedObject == null) {
                RaycastHit hit = CastRay();

                if(hit.collider != null) {
                    if (!hit.collider.CompareTag("drag")) {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    
                    Cursor.visible = false;
                }
            } else {
                selectedObject.GetComponent<BoxCollider>().enabled = false;
                RaycastHit hit2 =CastRay();
                if (hit2.collider !=null && hit2.collider.tag == "Buildable")
                {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, hit2.point.y, worldPosition.z);

                isPickedUp = false;
                selectedObject.GetComponent<BoxCollider>().enabled = true;
                selectedObject = null;
                Cursor.visible = true;
                
                }

            }
        }

        if(selectedObject != null) {
            isPickedUp = true;
            selectedObject.GetComponent<BoxCollider>().enabled = false;
            RaycastHit hit3 =CastRay();
            if (hit3.collider !=null && hit3.collider.tag == "Buildable"){
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x,
                hit3.point.y+0.25f, worldPosition.z);
            selectedObject.GetComponent<BoxCollider>().enabled = true;
            }
            
        }

    }

    private RaycastHit CastRay() {
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
