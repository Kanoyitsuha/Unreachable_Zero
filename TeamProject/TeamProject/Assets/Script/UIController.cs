using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public void ToogleMusic()
    {
        Music.instance.ToogleMusic();

    }

    public void ToogleSFX()
    {
        Music.instance.ToogleSFX();

    }


    public void MusicVolume()
    {
        Music.instance.musicVolume(musicSlider.value);

    }


    public void SFXVolume()
    {
        Music.instance.sfxVolume(sfxSlider.value);
    }


}
