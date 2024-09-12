using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private GameObject doorSprite;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private GameObject[] enemyToActive;

    private int enemiesRemaining;

    private void Start()
    {
        doorSprite.SetActive(false);
        foreach (GameObject enemyToActives in enemyToActive)
        {
            enemyToActives.SetActive(false);

        }
        enemiesRemaining = enemies.Length;

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnEnemyDeath += HandleEnemyDeath;
            }
        }
    }

    private void HandleEnemyDeath()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            AllEnemiesCleared();
        }
    }

    private void AllEnemiesCleared()
    {
        doorSprite.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && doorSprite.activeInHierarchy)
        {
            cam.MoveToNewRoom(nextRoom);
            ActivateEnemys();
        }
    }

    private void ActivateEnemys()
    {
        foreach (GameObject enemyToActives in enemyToActive)
        {
            enemyToActives.SetActive(true);
        }


    }
}