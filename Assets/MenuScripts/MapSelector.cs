using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MapSelector : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    
    public void SelectMap1(int sceneIndex)
    {
        StartCoroutine(LoadAsycnhronously(sceneIndex));
    }

    public void SelectMap2(int sceneIndex)
    {
        StartCoroutine(LoadAsycnhronously(sceneIndex));
    }
    
    public void SelectMap3(int sceneIndex)
    {
        StartCoroutine(LoadAsycnhronously(sceneIndex));
    }

    IEnumerator LoadAsycnhronously (int sceneIndex)
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);

            slider.value = progress;
            progressText.text = progress *100f+"%";

            yield return null;
        }
    }
}
