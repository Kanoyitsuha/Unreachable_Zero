using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TracingBullet : MonoBehaviour
{
    
    public GameObject target;
    [Range(1f,100f)]
    [SerializeField]public float speed = 50f;
    [SerializeField]public float rotateSpeed = 200f;
    public int damage = 50;
    private float bulletlife = 1f;
    public float SearchRadius = 20f;
    private bool IsHoming = false;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletlife);
        target = FindNearestEnemy();
        IsHoming = target != null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsHoming)
        {
            rb.velocity = transform.right * speed;
            target = FindNearestEnemy();
        }


        else if (target != null)
        {
            Vector2 direction = (Vector2)target.transform.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.right).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.right * speed;
        }

        else
        {
            IsHoming = false;
        }
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossShoot>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= SearchRadius)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;   
            }
            
        }
        return nearestEnemy;
    }
}
