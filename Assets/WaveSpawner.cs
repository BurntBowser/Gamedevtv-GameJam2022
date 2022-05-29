using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Transform objectPool;

    public float timeBetweenWaves = 5f;
    private float countdown = 3f;

    public TMPro.TextMeshProUGUI waveCountDownText;
    private int waveNumber = 1;
    bool pressedStart =false;
    
    void Update()
    {
        if(pressedStart == true)
        {
            StartWaves();
        }
    }

    public void PushStart(bool currentState)
    {
        pressedStart = currentState;

    }

    void StartWaves()
    {
        
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        waveCountDownText.text = Mathf.Ceil(countdown).ToString();
        countdown -= Time.deltaTime;
        
    }


    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }


       waveNumber++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, objectPool);
    }
}
