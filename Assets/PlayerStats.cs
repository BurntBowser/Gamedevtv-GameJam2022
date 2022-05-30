using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    
    public int Money;
    public int Lives = 100;
    public int startLives = 100;
    public int startMoney = 400;
    public TMPro.TextMeshProUGUI livesText;
    public TMPro.TextMeshProUGUI moneyText;
    public GameObject MasterUI;
    public GameObject gameOverUI;
    public GameObject winUI;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }

    void Update()
    {
        livesText.text = Lives.ToString();
        moneyText.text =  Money.ToString();

        if(Lives<=0)
        {
            MasterUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }

    public void WinGame()
    {
        MasterUI.SetActive(false);
        winUI.SetActive(true);
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
