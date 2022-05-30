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

    public TMPro.TextMeshProUGUI waveCountDownText;
    private int waveNumber = 1;
    bool pressedStart =false;
    PlayerStats stats;
    
    private void Start() 
    {
        stats = FindObjectOfType<PlayerStats>();
        enemyPool = new GameObject("Enemy Pool");
        waveCountDownText.text = waveNumber.ToString();
        waveCountDownText.color = Color.red;
    }
    
    void Update()
    {
        if(pressedStart == true && enemyPool.transform.childCount<=0 && waveNumber <11)
        {
            StartWaves(waveNumber);
        }
        else if (pressedStart != true)
        {
            return;
        }

        if(waveNumber>=11)
        {
            stats.WinGame();
        }
        waveCountDownText.text = waveNumber.ToString();
        waveCountDownText.color = Color.white;
        
    }

    public void PushStart()
    {
        pressedStart =!pressedStart;
        waveCountDownText.color = Color.red;

    }

    void StartWaves(int wave)
    {
        
        if (wave <11)
        {
            SpawnWave(wave);
            waveNumber++;
        }
        
    }


    void SpawnWave(int wave)
    {
        switch (wave)
        {
            case 1:
                StartCoroutine(SpawnEnemy(enemyDefault, 3, 2f));
                break;
            case 2:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                break;
            case 3:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 0.5f));
                break;
            case 4:
                StartCoroutine(SpawnEnemy(enemyDefault, 10, 1f));
                break;
            case 5:
                StartCoroutine(SpawnEnemy(enemyDefault, 10, 0.5f));
                StartCoroutine(SpawnEnemy(enemyMedium, 2, 0.5f));
                break;
            case 6:
                StartCoroutine(SpawnEnemy(enemyDefault, 15, 1f));
                StartCoroutine(SpawnEnemy(enemyMedium, 3, 0.8f));
                break;
            case 7:
                StartCoroutine(SpawnEnemy(enemyDefault, 13, 0.7f));
                StartCoroutine(SpawnEnemy(enemyMedium, 6, 1f));
                StartCoroutine(SpawnEnemy(enemyFast, 4, 2f));
                break;
            case 8:
                StartCoroutine(SpawnEnemy(enemyDefault, 10, 1f));
                StartCoroutine(SpawnEnemy(enemyMedium, 8, 1f));
                StartCoroutine(SpawnEnemy(enemyFast, 5, 2f));
                break;
            case 9:
                StartCoroutine(SpawnEnemy(enemyDefault, 12, 1f));
                StartCoroutine(SpawnEnemy(enemyMedium, 10, 1f));
                StartCoroutine(SpawnEnemy(enemyFast, 7, 2f));
                StartCoroutine(SpawnEnemy(enemyBig, 1, 5f));
                break;
            case 10:
                StartCoroutine(SpawnEnemy(enemyDefault, 5, 1f));
                StartCoroutine(SpawnEnemy(enemyMedium, 6, 1f));
                StartCoroutine(SpawnEnemy(enemyFast, 12, 2f));
                StartCoroutine(SpawnEnemy(enemyBig, 2, 5f));
                break;
            case 11:
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
