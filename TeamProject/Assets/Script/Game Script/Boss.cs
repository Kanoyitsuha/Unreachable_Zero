using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 100;
    public GameObject[] ItemDrops;
    public AudioClip damageSound;
    private AudioSource audioSource;

    public enum SpawnerType
    {
        Straight,
        Spin,
        Circular,
        Spread,
        Spiral
    }

    public enum MovementType
    {
        EnterScene,
        Patrol,
        Wander,
        Sinusoidal,
        UpDown
    }
    [Header("Movement Attributes")]
    public MovementType movementType = MovementType.EnterScene;
    public float moveSpeed = 2f;
    public Vector2 entryPoint = new Vector2(0, 0);
    public float patrolDistance = 5f;
    public float wanderRadius = 3f;
    public float wanderInterval = 2f;
    public float frequency = 1f;
    public float amplitude = 1f;

    private bool movingRight = true;
    private Vector2 startPosition;
    private float patrolStartX;
    private float patrolEndX;

    public bool hasEnteredScene = false;
    private Vector2 targetPosition;
    private float wanderTimer;
    private float originalY;

    [Header("Spawner Attributes")]
    public SpawnerType spawnerType = SpawnerType.Straight;
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

    public delegate void EnemyDeath();
    public event EnemyDeath OnEnemyDeath;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Prevents the sound from playing automatically on start
        audioSource.clip = damageSound;  // Sets the damage sound clip to the AudioSource
        startPosition = transform.position;
        patrolStartX = entryPoint.x - patrolDistance;
        patrolEndX = entryPoint.x + patrolDistance;
        wanderTimer = wanderInterval;
        originalY = transform.position.y;
    }

    void Update()
    {
        if (!hasEnteredScene)
        {
            MoveIntoScene();
        }
        else
        {
            switch (movementType)
            {
                case MovementType.Patrol:
                    PatrolMovement();
                    break;
                case MovementType.Wander:
                    WanderMovement();
                    break;
                case MovementType.Sinusoidal:
                    SinusoidalMovement();
                    break;
                case MovementType.UpDown:
                    UpDownMovement();
                    break;
            }

            timer += Time.deltaTime;
            if (timer >= firingRate)
            {
                SpawnBullets();
                timer = 0f; // Reset the timer after firing
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        PlayDamageSound();
        if (health <= 0)
            Die();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Charm1>())
            TakeDamage(1000);
    }


    private void Die()
    {
        ItemDrop();
        Destroy(gameObject);
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath.Invoke();
        }


    }

    private void PlayDamageSound()
    {

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);  // Play the damage sound effect
        }
        else
        {
            Debug.LogWarning("AudioSource or damage sound is not assigned!");
        }
    }

    private void ItemDrop()
    {
        float randomNum = Random.Range(0, 20);
        switch (randomNum)
        {
            case 1:
                Instantiate(ItemDrops[1], transform.position, Quaternion.identity);
                break;
            case 2:
                break;
            case 3:
                Instantiate(ItemDrops[2], transform.position, Quaternion.identity);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                Instantiate(ItemDrops[3], transform.position, Quaternion.identity);
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                Instantiate(ItemDrops[4], transform.position, Quaternion.identity);
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            default:
                Instantiate(ItemDrops[0], transform.position, Quaternion.identity);
                break;

        }

    }

    void SpawnBullets()
    {
        switch (spawnerType)
        {
            case SpawnerType.Straight:
                FireStraight();
                break;
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

    void FireStraight()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
        Destroy(bullet, bulletLife);
    }

    void FireSpin()
    {
        float angleStep = 360f / bulletsPerRound;
        float angle = spinAngle;

        for (int i = 0; i < bulletsPerRound; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * bulletSpeed;
            Destroy(bullet, bulletLife);
            angle += angleStep;
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
        float angleStep = spiralRotationSpeed * Time.deltaTime;
        spiralAngle += angleStep;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, spiralAngle));
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-Mathf.Cos(spiralAngle * Mathf.Deg2Rad), -Mathf.Sin(spiralAngle * Mathf.Deg2Rad)) * bulletSpeed;
        Destroy(bullet, bulletLife);
    }
    void MoveIntoScene()
    {
        transform.position = Vector2.MoveTowards(transform.position, entryPoint, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == entryPoint)
        {
            hasEnteredScene = true;

            if (movementType == MovementType.UpDown)
            {
                originalY = transform.position.y;
            }
        }
    }



    void PatrolMovement()
    {
        float newX = transform.position.x + (movingRight ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime);
        transform.position = new Vector2(newX, transform.position.y);

        if (transform.position.x >= patrolEndX)
        {
            movingRight = false;
        }
        else if (transform.position.x <= patrolStartX)
        {
            movingRight = true;
        }
    }

    void WanderMovement()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * wanderRadius;
            targetPosition = new Vector2(entryPoint.x + randomDirection.x, entryPoint.y + randomDirection.y);
        }

        transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void SinusoidalMovement()
    {
        float newX = transform.position.x + moveSpeed * Time.deltaTime;
        float newY = originalY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector2(newX, newY);
    }


    void UpDownMovement()
    {
        float newY = originalY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector2(transform.position.x, newY);
    }
}