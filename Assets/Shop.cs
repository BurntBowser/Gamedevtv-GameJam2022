using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    PlacementHandler placement;
    PlayerStats stats;
    public GameObject currentUnit;
    public GameObject Unit;


    void Start() 
    {
        placement = FindObjectOfType<PlacementHandler>();
        stats = FindObjectOfType<PlayerStats>();
    }

    public void SelectDefaultAlly()
    {
        if(placement.DefaultAlly.isLocked != true && stats.Money >= placement.DefaultAlly.cost)
        {
        Debug.Log("Selected Default Ally");
        placement.SpawnObject(placement.DefaultAlly);
        }
        else
        {
            DenyUser(placement.DefaultAlly.prefab.name);
        }
    }

    public void SelectLee()
    {
        if(placement.Lee.isLocked != true && stats.Money >= placement.Lee.cost)
        {
        Debug.Log("Selected Lee");
        placement.SpawnObject(placement.Lee);
        }
        else
        {
            DenyUser(placement.Lee.prefab.name);
        }
    }
    public void SelectBigGuy()
    {
        if(placement.BigGuy.isLocked != true && stats.Money >= placement.BigGuy.cost)
        {
        Debug.Log("Selected Big Guy");
        placement.SpawnObject(placement.BigGuy);
        }
        else
        {
            DenyUser(placement.BigGuy.prefab.name);
        }
    }
    public void SelectGerad()
    {
        if(placement.Gared.isLocked != true && stats.Money >= placement.Gared.cost)
        {
        Debug.Log("Selected Gerad");
        placement.SpawnObject(placement.Gared);
        }
        else
        {
            DenyUser(placement.Gared.prefab.name);
        }
    }
    public void SelectFifthOption()
    {
        if(placement.Thomas.isLocked != true && stats.Money >= placement.Thomas.cost)
        {
        Debug.Log("Selected Fifth Option");
        placement.SpawnObject(placement.Thomas);
        }
        else
        {
            DenyUser(placement.Thomas.prefab.name);
        }
    }

    public void DenyUser(String towername)
    {
        Debug.Log($"Cannot select {towername}, as it is currently locked.");
    }
    
}
