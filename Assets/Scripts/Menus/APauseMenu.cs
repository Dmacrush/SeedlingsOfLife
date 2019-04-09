using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APauseMenu : MonoBehaviour
{
    public GameObject PauseMenuHolder;
    public static bool GameIsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenuHolder.SetActive(true);
        GameIsPaused = true;
        
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenuHolder.SetActive(false);
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        
    }
}
