using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    public AudioSource titleMusic;  // Assign in Inspector
    public AudioSource tutorialMusic;
    public AudioSource gameMusic;   // Assign in Inspector

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Title")
        {
            PlayMusic(titleMusic);
        }
        else if (currentScene.name == "Tutorial")
        {
            PlayMusic(tutorialMusic);
        }

        else if (currentScene.name == "Game1")
        {
            PlayMusic(gameMusic);
        }
    }

    private void PlayMusic(AudioSource music)
    {
        // Stop any currently playing music
        if (titleMusic.isPlaying) 
        { 
            gameMusic.Stop();
            tutorialMusic.Stop(); 
        }

        if (tutorialMusic.isPlaying)
        {
            titleMusic.Stop();
            gameMusic.Stop();
        }

        if (gameMusic.isPlaying)
        {
            titleMusic.Stop();
            tutorialMusic.Stop();
        }


        // Play the new music
        music.Play();
    }
}