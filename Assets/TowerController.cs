using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameObject infoUI;
    [SerializeField] GameObject particles;
    private Transform target;
    
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public Targeting targeting;
    public AllyName towerName;

    [Header("Unity Setup Fields")]
    public string enemyTag = "enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPreFab;
    public GameObject rockPreFab;
    public Transform firePoint;
    public GameObject objectPool;

    PlacementHandler placement;
    UIController uiControl;

    public bool isPanelOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
        placement = FindObjectOfType<PlacementHandler>();
        objectPool = new GameObject("Object Pool");
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
        if (gameObject.tag == "placing") {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            return;}
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

    private void OnMouseDown() 
    {
        
    }
    
    void Shoot()
    {
        if((int)towerName == 0) //default ally, shoots projectiles that follow, consistent damage
        {
           GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation, objectPool.transform);
           Projectile bullet = bulletGO.GetComponent<Projectile>();
           
           if(bullet != null)
           {
               bullet.SetDamage(placement.DefaultAlly.damage);
               bullet.Seek(target);
           }
           return;
        }
        if((int)towerName == 1) //Big Guy, attacks close, all in radius.
        {
            AttackInRadius(placement.BigGuy.damage);
            return;
        }
        if((int)towerName == 2) //Lee, attacks close, single target 
        {
           GameObject rockGO = (GameObject)Instantiate(rockPreFab, firePoint.position, firePoint.rotation, objectPool.transform);
           Projectile rock = rockGO.GetComponent<Projectile>();
           
           if(rock != null)
           {
               rock.SetDamage(placement.Lee.damage);
               rock.Seek(target);
           }
           return;
        }
        if((int)towerName == 3) //Gared, attacks anywhere on the map. Raycast to target. blocked by teammates.
        {
            RaycastHit hit;
            Physics.Raycast(firePoint.transform.position,target.transform.position-firePoint.transform.position, out hit,100f);
            if(hit.collider.tag == "enemy")
            {
                hit.transform.GetComponent<EnemyController>().TotalHealth -= placement.Gared.damage;
            }
            return;
        }
        if((int)towerName == 4) //Thomas, does something
        {
            AttackInBox(placement.Thomas.damage);
            return;
        }
        else
        {
            Debug.Log("The name has not been set, fix this before make game.");
        }
    }

    void AttackInBox(int damage)
    {
        Collider[] colliders = Physics.OverlapBox(firePoint.transform.position,new Vector3(2f,2f,2f),transform.rotation);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "enemy")
            {
                collider.GetComponent<EnemyController>().TotalHealth -=damage;
            }
        }
    }

    void AttackInRadius(int damage)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,range);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "enemy")
            {
                collider.GetComponent<EnemyController>().TotalHealth -=damage;
            }
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        if((int)towerName == 4)
        {
            Gizmos.DrawWireCube(firePoint.transform.position,new Vector3(4f,4f,4f));
        }
    }
}
