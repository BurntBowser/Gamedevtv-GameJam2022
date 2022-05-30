using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float radiusOfEffect = 0f;
    public int dealThisDamage = 0;
    private float initilizeTime = 0f;
    public float flightTime = 2f;

    void OnEnable()
    {
        initilizeTime = Time.time;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed*Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        if(Time.time - initilizeTime >= flightTime)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized*distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    public void SetDamage(int damage)
    {
        dealThisDamage = damage;
    }

    void HitTarget()
    {
        if (radiusOfEffect >0f)
        {
            Explode();
        }
        else
        {
            Damage(target, dealThisDamage);
        }
        
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,radiusOfEffect);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "enemy")
            {
                Damage(collider.transform, dealThisDamage);
            }
        }
    }

    public void Damage(Transform enemy, int damage)
    {
        enemy.GetComponent<EnemyController>().TotalHealth -= damage;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfEffect);
    }

}
