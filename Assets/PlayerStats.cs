using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public static int Money;
    public int Lives = 100;
    public int startMoney = 400;

    void Start()
    {
        Money = startMoney;
    }

    public void PayUp(int cost)
    {
        Money -= cost;
        Debug.Log("We lost money, and now have " + Money);
    }

    public void GiveReward(int reward)
    {
        Money += reward;
        Debug.Log("We gained money, and now have " + Money);
    }

}
