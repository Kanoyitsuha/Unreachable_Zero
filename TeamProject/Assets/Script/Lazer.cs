using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lazer : MonoBehaviour {

[Range(0f, 100f)]
[SerializeField] public float speed = 100f;

[Range(0f, 1000f)]
[SerializeField] public int damage = 100;

[Range(0f, 10f)]
[SerializeField] public float Bulletlife = 2f;

private Rigidbody2D rb;

private void Start()
{
    rb = GetComponent<Rigidbody2D>();
    Destroy(gameObject, Bulletlife);
}


private void Update()
{
    rb.velocity = transform.right * speed;

}

void OnTriggerEnter2D(Collider2D hitInfo)
{
    Enemy enemy = hitInfo.GetComponent<Enemy>();
    if (enemy != null)
    {
        enemy.TakeDamage(damage);
        
    }

}




}
