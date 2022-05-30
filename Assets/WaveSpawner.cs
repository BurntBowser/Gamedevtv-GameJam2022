using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    
    public GameObject enemyDefault;
    public GameObject enemyBig;
    public GameObject enemyMedium;
    public GameObject enemyFast;

    public Transform spawnPoint;
    public GameObject enemyPool;

    public float timeBetweenWaves = 5f;
    private float countdown = 3f;

    public TMPro.TextMeshProUGUI waveCountDownText;
    private int waveNumber = 1;
    bool pressedStart =false;
    
    private void Start() 
    {
        enemyPool = new GameObject("Enemy Pool");
    }
    
    void Update()
    {
        if(pressedStart == true)
        {
            StartWaves(waveNumber);
        }
        else if (pressedStart != true)
        {
            PauseWaves();
        }
        waveCountDownText.text = Mathf.Ceil(countdown).ToString();
        
    }

    void PauseWaves()
    {
        countdown = timeBetweenWaves;
    }

    public void PushStart()
    {
        pressedStart =!pressedStart;

    }

    void StartWaves(int wave)
    {
        
        if (countdown <= 0f)
        {
            SpawnWave(wave);
            countdown = timeBetweenWaves;
            waveNumber++;
        }
        countdown -= Time.deltaTime;
        
    }


    void SpawnWave(int wave)
    {
        switch (wave)
        {
            case 1:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
            case 2:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 0.5f));
                break;
            case 3:
                StartCoroutine(SpawnEnemy(enemyDefault, 3, 0.2f));
                break;
            case 4:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
            case 5:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
            case 6:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
            case 7:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
            case 8:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
        }
    }

    IEnumerator SpawnEnemy(GameObject enemyPreFab, int times, float seconds)
    {
        for (int i = 0; i < times; i++)
        {
            Instantiate(enemyPreFab, spawnPoint.position, spawnPoint.rotation, enemyPool.transform);
            yield return new WaitForSeconds(seconds);
        }
    }
}
