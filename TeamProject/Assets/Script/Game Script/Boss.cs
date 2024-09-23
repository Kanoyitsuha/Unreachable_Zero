using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public enum SpawnerType
    {
        Spin,
        Circular,
        Spread,
        Spiral
    }

    [Header("Spawner Attributes")]
    public SpawnerType spawnerType = SpawnerType.Spin;
    public GameObject bulletPrefab;
    public float firingRate = 1.0f;
    public float bulletSpeed = 7.0f;
    public float bulletLife = 1.0f;

    [Header("Circular Pattern")]
    public int bulletsPerRound = 8;

    [Header("Spread Pattern")]
    public int bulletsPerSpread = 5;
    public float spreadAngle = 45f;

    [Header("Spiral Pattern")]
    public float spiralRotationSpeed = 30f;

    private float timer = 0f;
    private float spinAngle = 0f;
    private float spiralAngle = 0f;

    [Header("Shooting Type Change Attributes")]
    public float changeInterval = 10f;
    private float changeTimer = 0f;

    [Header("Health Attributes")]
    public int maxHealth = 10000;
    private int currentHealth;
    private bool isActive = true;

    public delegate void OnDeath();
    public event OnDeath OnDeathEvent;

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        changeTimer += Time.deltaTime;

        if (timer >= firingRate)
        {
            SpawnBullets();
            timer = 0;
        }

        if (changeTimer >= changeInterval)
        {
            RandomizeSpawnerType();
            changeTimer = 0;
        }
    }

    void SpawnBullets()
    {
        switch (spawnerType)
        {
            case SpawnerType.Spin:
                FireSpin();
                break;
            case SpawnerType.Circular:
                FireCircular();
                break;
            case SpawnerType.Spread:
                FireSpread();
                break;
            case SpawnerType.Spiral:
                FireSpiral();
                break;
        }
    }

    void FireSpin()
    {
        float angleStep = 360f / bulletsPerRound;
        float spiralRadius = 0.1f;
        float currentAngle = spinAngle;

        for (int i = 0; i < bulletsPerRound; i++)
        {
            float bulletDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            Vector2 spawnPosition = new Vector2(transform.position.x + bulletDirX * spiralRadius, transform.position.y + bulletDirY * spiralRadius);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            spiralRadius += 0.1f;

            Destroy(bullet, bulletLife);

            currentAngle += angleStep;
        }

        spinAngle += 10f;
    }

    void FireCircular()
    {
        float angleStep = 360f / bulletsPerRound;
        float angle = 0f;

        for (int i = 0; i < bulletsPerRound; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * bulletSpeed;
            Destroy(bullet, bulletLife);
            angle += angleStep;
        }
    }

    void FireSpread()
    {
        float angleStep = spreadAngle / (bulletsPerSpread - 1);
        float baseAngle = 180f;

        for (int i = 0; i < bulletsPerSpread; i++)
        {
            float angle = baseAngle + (i - (bulletsPerSpread - 1) / 2.0f) * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = rotation * Vector2.right * bulletSpeed;
            Destroy(bullet, bulletLife);
        }
    }

    void FireSpiral()
    {
        int bulletsPerRound = 8;
        spiralAngle += spiralRotationSpeed;
        float angleStep = 360f / bulletsPerRound;

        for (int i = 0; i < bulletsPerRound; i++)
        {
            float currentAngle = spiralAngle + (i * angleStep);
            float bulletDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
            Destroy(bullet, bulletLife);
        }

        for (int i = 0; i < bulletsPerRound; i++)
        {
            float currentAngle = spiralAngle + (i * angleStep);
            float bulletDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            Vector2 spawnPosition = new Vector2(transform.position.x + bulletDirX * 0.5f, transform.position.y + bulletDirY * 0.5f);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
            Destroy(bullet, bulletLife);
        }
    }

    void RandomizeSpawnerType()
    {
        spawnerType = (SpawnerType)Random.Range(0, System.Enum.GetValues(typeof(SpawnerType)).Length);
    }

    public void TakeDamage(int damage)
    {
        if (!isActive) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        PlayExplosionSound();
    }
        void Die()
        {
            isActive = false;

            if (OnDeathEvent != null)
            {
                OnDeathEvent.Invoke();
            }
            Music.instance.PlaySE("Easter Egg");
            Destroy(gameObject);
        }
        private void PlayExplosionSound()
        {

            float randomValue = Random.Range(0f, 1f);

            if (randomValue < 0.01f)
            {
                Music.instance.PlaySE("Easter Egg");
            }
            else
            {
                Music.instance.PlaySE("Enemy Hit");
            }
        }
    
}
