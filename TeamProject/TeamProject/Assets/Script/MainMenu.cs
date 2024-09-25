using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{



    public GameObject settingPanel;
    public GameObject MainMenuPanel;
    public TMP_Text MainMenuText;
    public AudioMixer myMixer;
    public Slider volSlider;
    public float currentVolume;
    public Toggle muteTrigger;



    public float delay = 2f;  // Delay in seconds before loading the game scene
    public void StartGame()
    {
        StartCoroutine(LoadSceneAfterDelay(1));
    }

    public void SettingMenu()
    {
        settingPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainMenuText.text = "Setting";

    }

    public void QuitSetting()
    {
        settingPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        MainMenuText.text = "Main Menu";

    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void myvolume(float sliderValue)
    {
        myMixer.SetFloat("myVolume", Mathf.Log10(sliderValue) * 10);

    }
    IEnumerator LoadSceneAfterDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene(sceneIndex);     // Load the specified scene
    }
}
