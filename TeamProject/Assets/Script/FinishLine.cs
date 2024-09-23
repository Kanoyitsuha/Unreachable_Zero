using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject finishLineSprite;
    [SerializeField] private Enemy[] enemies;

    private int enemiesRemaining;

    private void Start()
    {
        finishLineSprite.SetActive(false);
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
        finishLineSprite.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && finishLineSprite.activeInHierarchy)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("Congratulations! You've completed the game.");
            if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("Congratulations! You've completed the game.");
            }
        }
    }
}