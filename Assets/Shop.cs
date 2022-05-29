using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("GameObjects that correspond to Allies")]
    public GameObject DefaultAlly;
    public GameObject Lee;
    public GameObject BigGuy;
    public GameObject Gerad;
    public GameObject Fifth;

    GameObject towerPool;

    void Start() 
    {
        towerPool = new GameObject("TowerPool");
    }

    public void PurchaseDefaultAlly()
    {
        Debug.Log("Purchased Default Ally");
        GameObject unit = DefaultAlly;
        PurchaseUnit(unit);
    }

    public void PurchaseLee()
    {
        Debug.Log("Purchased Lee");
    }
    public void PurchaseBigGuy()
    {
        Debug.Log("Purchased Big Guy");
    }
    public void PurchaseGerad()
    {
        Debug.Log("Purchased Gerad");
    }
    public void PurchaseFifthOption()
    {
        Debug.Log("Purchased Fifth Option");
    }

    public void PurchaseUnit(GameObject chosenUnit)
    {
        GameObject unit = (GameObject)Instantiate(DefaultAlly,Vector3.zero, Quaternion.identity, towerPool.transform);
        
    }

}
