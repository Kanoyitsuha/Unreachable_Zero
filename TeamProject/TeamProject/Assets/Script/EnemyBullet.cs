using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    void Update()
    {
        CheckIfOutOfBounds();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                Destroy(gameObject);
            }
            
        
        if (collision.CompareTag("Ground") )
        {
            Destroy(gameObject);
        }
    }

    void CheckIfOutOfBounds()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }
}