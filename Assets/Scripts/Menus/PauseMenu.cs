using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuHolder;

    public void PauseGameKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuHolder.SetActive(true);
            Time.timeScale = 0;
        }
        
    }

    public void PauseGameButton()
    {
        PauseMenuHolder.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PauseMenuHolder.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
