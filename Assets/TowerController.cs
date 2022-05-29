using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameObject infoUI;
    [SerializeField] GameObject particles;
    private Transform target;
    
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPreFab;
    public Transform firePoint;
    public GameObject objectPool;

    public string towerName = "WRITENAMEHERE";
    Grabber grabber;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
        grabber = FindObjectOfType<Grabber>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            return;
        }
        else 
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {return;}
        if (grabber.isPickedUp == true) {return;}
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime*turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f,rotation.y,0f);

        if (fireCountdown <=0.01f)
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        if(towerName.ToLower() == "default")
        {
           GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation, objectPool.transform);
           Projectile bullet = bulletGO.GetComponent<Projectile>();
           
           if(bullet != null)
           {
               bullet.Seek(target);
           }
           return;
        }
        if(towerName.ToLower() == "lee")
        {return;}
        if(towerName.ToLower() == "big guy")
        {return;}
        if(towerName.ToLower() == "gerad")
        {return;}
        if(towerName.ToLower() == "fifth")
        {return;}
        else
        {
            Debug.Log("The name has not been set, fix this before make game.");
        }
    }

    void Placement()
    {
        //called when placed
        //when called, get tower from queue (queue is made already)
        //tower is not active yet, but will get a public bool turned true once placed.
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
