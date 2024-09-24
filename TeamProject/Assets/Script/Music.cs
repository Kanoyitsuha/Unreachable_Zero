using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Music : MonoBehaviour
{
    public static Music instance;
    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;
    private int currentSceneIndex;

    private float currentTime = 0;
    private string currentMusicName = "";


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        PlayMusic("Title");
    }

    private void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        switch (currentSceneIndex)
        {
            case 1:
                PlayMusic("CutScene 1");
                break;
            case 2:
                PlayMusic("Title");
                break;
            case 3:
            case 4:
                PlayMusic("Game");
                break;
            case 5:
                randomMusic();
                break;
            case 6:
                PlayMusic("CutScene 2");
                break;

        }
        //if (currentSceneIndex == 2 && !musicChangedForScene1)
        //{
        //    PlayMusic("Game");
        //    musicChangedForScene1 = true;
        //}

        //if (currentSceneIndex == 0 && musicChangedForScene1)
        //{
        //    PlayMusic("Title");
        //    musicChangedForScene1 = false;
        //}

    }

    private void PlayMusicIfNotAlreadyPlaying(string musicName)
    {
        if (currentMusicName != musicName)  // Only change music if it's different
        {
            PlayMusic(musicName);
            currentMusicName = musicName;  // Track currently playing music
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Didn't have Sound");
        }
        else
        {
            musicSource.Stop();  // Stop any current music
            musicSource.clip = s.clip;
            musicSource.time = 0;
            musicSource.Play();
            Debug.Log("Playing clip: " + s.clip.name);
        }
    }

    public void ContinueMusic()
    {
        if (musicSource.clip != null)
        {
            musicSource.time = currentTime;
            musicSource.Play();

        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            currentTime = musicSource.time;
            musicSource.Pause();
        }
    }


    public void PlaySE(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Didn't have Sound");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToogleMusic()
    {
        musicSource.mute = !musicSource.mute;

    }

    public void ToogleSFX()
    {
        sfxSource.mute = !sfxSource.mute;

    }
    public void musicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void sfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    public void StopMusic()
    {
        musicSource.Stop();
        currentTime = 0;
    }
    void randomMusic()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        if (randomValue < 0.3f)
            Music.instance.PlayMusic("Boss 1");
        if (randomValue < 0.6f && randomValue > 0.3f)
            Music.instance.PlayMusic("Boss 2");
        else
            Music.instance.PlayMusic("Boss 3");

    }
}