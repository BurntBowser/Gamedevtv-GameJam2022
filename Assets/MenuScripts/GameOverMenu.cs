using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private int currentMap;
    public PlayerStats stats;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        currentMap = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadAsycnhronously(0));
        StartCoroutine(LoadAsycnhronously(currentMap));
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

IEnumerator LoadAsycnhronously (int sceneIndex)
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);

            yield return null;
        }
    }

}
