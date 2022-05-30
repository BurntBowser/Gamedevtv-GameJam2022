using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private Scene currentMap;
    public PlayerStats stats;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        currentMap = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentMap.name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
