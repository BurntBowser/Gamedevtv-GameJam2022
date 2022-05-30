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
    public GameObject pauseUI;
    public bool isPaused = false;

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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if(isPaused ==true)
            {
                MasterUI.SetActive(false);
                pauseUI.SetActive(true);
                Time.timeScale=0f;
                return;
            }
            else
            {
                MasterUI.SetActive(true);
                pauseUI.SetActive(false);
                Time.timeScale=1f;
                return;
            }
            
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
