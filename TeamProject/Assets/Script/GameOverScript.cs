using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameoverUI;


    public void GameOver()
    {

        gameoverUI.SetActive(true);
        Music.instance.PlayMusic("Game Over");

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Music.instance.StopMusic();
        Music.instance.PlayMusic("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
