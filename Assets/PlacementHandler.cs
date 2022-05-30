using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlacementHandler : MonoBehaviour
{
    [Header("All placeable towers")]
    public TowerBlueprints DefaultAlly;
    public TowerBlueprints Lee;
    public TowerBlueprints BigGuy;
    public TowerBlueprints Gared;
    public TowerBlueprints Thomas;

    static GameObject unit;
    public int currentCost;

    GameObject towerPool;
    PlayerStats stats;

    void Start()
    {
        towerPool = new GameObject("TowerPool");
        stats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        if(unit!=null && unit.tag == "placing")
        {
            Cursor.visible = false;
            unit.GetComponent<BoxCollider>().enabled = false;
            unit.GetComponent<NavMeshObstacle>().enabled = false;
            RaycastHit hit = CastRay();
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(unit.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            unit.transform.position = new Vector3(worldPosition.x, hit.point.y+0.25f, worldPosition.z);

            if(Input.GetMouseButtonDown(0))
            {
                if(hit.collider.tag == "Buildable")
                {
                unit.transform.position = new Vector3(worldPosition.x, hit.point.y, worldPosition.z);
                unit.tag = "Untagged";
                unit.GetComponent<BoxCollider>().enabled = true;
                unit.GetComponent<NavMeshObstacle>().enabled = true;
                
                unit = null;
                stats.PayUp(currentCost);
                Cursor.visible = true;

                }
            }
        }
    }

    public void SpawnObject(TowerBlueprints unitChosen)
    {
        unit = (GameObject)Instantiate(unitChosen.prefab,Vector3.zero, Quaternion.identity, towerPool.transform);
        unit.tag = "placing";
        currentCost = unitChosen.cost;
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
