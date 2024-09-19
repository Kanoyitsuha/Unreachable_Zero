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
    private bool musicChangedForScene1 = false;


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

        if (currentSceneIndex == 2 && !musicChangedForScene1)
        {
            PlayMusic("Game");
            musicChangedForScene1 = true;
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
            musicSource.clip = s.clip;
            musicSource.Play();

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


}
