using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject playMenuHolder;
    
    public GameObject optionsMenuHolder;
    public GameObject quitHolder;
    public GameObject loadLevelSelect;

    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle fullScreenToggle;
    public int[] screenWidths;

    int activeScreenResIndex;

    LevelScript levelScript;

    private void Start()
    {
        levelScript = FindObjectOfType<LevelScript>();
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1 ? true : false);

        volumeSliders[0].value = AudioManager.instance.MasterVolumePercent;

        volumeSliders[0].value = AudioManager.instance.MasterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.MusicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.SfxVolumePercent;

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = 1 == activeScreenResIndex;

        }

        fullScreenToggle.isOn = isFullscreen;
    }

    public void PlayGame()
    {
        //put load level here when we finialise names
        
    }

    public void DisplayPlayMenu()
    {
        playMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
        quitHolder.SetActive(false);
        loadLevelSelect.SetActive(false);
    }

    public void DisplayQuitMenu()
    {
        playMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(false);
        quitHolder.SetActive(true);
        loadLevelSelect.SetActive(false);
    }

    public void DisPlayOptionsMenu()
    {
        playMenuHolder.SetActive(false);
        quitHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
        loadLevelSelect.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene("Battlelevel");
    }

    public void LoadLevelSelect()
    {
        loadLevelSelect.SetActive(true);
        playMenuHolder.SetActive(false);

        //if we want to save last played level
        /*
        if (PlayerPrefs.GetInt("Level Completed") >= 1)
        {
            levelScript.CurrentLevel = PlayerPrefs.GetInt("Level Completed");
            SceneManager.LoadScene(levelScript.CurrentLevel);

        }
        else
        {
            SceneManager.LoadScene(1);    
        }
        */

    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
}

