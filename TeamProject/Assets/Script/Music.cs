using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public static Music instance;
    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;

    private int previousSceneIndex = -1;  // Store the previously active scene
    private string currentMusicName = "";
    private float currentTime = 0;

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
        PlayMusicIfNotAlreadyPlaying("Title");  // Default music for title screen
    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if the scene has changed
        if (currentSceneIndex != previousSceneIndex)
        {
            OnSceneChanged(currentSceneIndex);  // Change music based on the scene
            previousSceneIndex = currentSceneIndex;  // Update the previous scene
        }
    }

    // Handles changing music when the scene is switched
    private void OnSceneChanged(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                PlayMusicIfNotAlreadyPlaying("Title");
                break;
            case 1:
                PlayMusicIfNotAlreadyPlaying("CutScene 1");
                break;
            case 2:
                PlayMusicIfNotAlreadyPlaying("Title");
                break;
            case 3:
            case 4:
                PlayMusicIfNotAlreadyPlaying("Game");
                break;
            case 5:
                PlayRandomMusic();
                break;
            case 6:
                PlayMusicIfNotAlreadyPlaying("CutScene 2");
                break;
            default:
                Debug.LogWarning("No specific music set for this scene.");
                break;
        }
    }

    private void PlayMusicIfNotAlreadyPlaying(string musicName)
    {
        if (currentMusicName != musicName)  // Change music only if it hasn't been played
        {
            PlayMusic(musicName);
            currentMusicName = musicName;  // Store the current playing music
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound '{name}' not found in the music list.");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.time = 0;  // Start the clip from the beginning
        musicSource.Play();
        currentMusicName = name;  // Track the currently playing music
        Debug.Log($"Playing music: {s.clip.name}");
    }

    private void PlayRandomMusic()
    {
        int randomIndex = UnityEngine.Random.Range(0, 3);
        string[] randomTracks = { "Boss 1", "Boss 2", "Boss 3" };
        PlayMusicIfNotAlreadyPlaying(randomTracks[randomIndex]);
    }

    // Other methods (ContinueMusic, PauseMusic, PlaySE, ToggleMusic, etc.) stay the same...



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
            sfxSource.PlayOneShot(s.clip, sfxSource.volume / 2);
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
    
}