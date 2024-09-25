using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnterScene : MonoBehaviour
{
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

    void Start()
    {
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
        }
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