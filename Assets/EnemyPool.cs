using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool 
    {
        public int size;
        public string tagName;
        public GameObject enemyPrefab;

    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start() 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i=0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.enemyPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }
    }

    //When called by start button
    //populate pool

    void PopulatePool()
    {
        
    }
    
    
}
