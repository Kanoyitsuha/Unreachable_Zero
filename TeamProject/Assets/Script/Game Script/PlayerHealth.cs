using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;
    public Weapon weapon;
    public GameObject explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
            Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(1);

        if (collision.gameObject.CompareTag("Heal"))
            Heal(1);

        if (collision.gameObject.CompareTag("Fan"))
        {
            weapon.WeaponType = 3;
            weapon.ResetWeaponTimer();
            InventorySwitch.itemdropswitch = 1;
            
        }
            
        if (collision.gameObject.CompareTag("Lazer"))
        { 
            weapon.WeaponType = 1;
            weapon.ResetWeaponTimer();
            InventorySwitch.itemdropswitch = 2;

        }

        if (collision.gameObject.CompareTag("Tracking"))
        {
            weapon.WeaponType = 2;
            weapon.ResetWeaponTimer();
            InventorySwitch.itemdropswitch = 3;

        }

        if (collision.gameObject.CompareTag("Charm"))
            charmmanager.charmCount++;



    }




    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth<0) 
            currentHealth = 0;
    }


    public void Heal(int healing)
    {
        currentHealth += healing;
        healthBar.SetHealth(currentHealth);

        if(currentHealth>maxHealth) 
            currentHealth = maxHealth;


    }

 private void Die()
    {
        Destroy(gameObject);
      
        
            // Instantiate the explosion at the character's position
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        
    }

}
