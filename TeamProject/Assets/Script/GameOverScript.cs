using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameoverUI;

    public void GameOver()
    {
        Time.timeScale = 0;
        gameoverUI.SetActive(true);
        Music.instance.PlayMusic("Game Over");

    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game1");
        Music.instance.StopMusic();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
}
