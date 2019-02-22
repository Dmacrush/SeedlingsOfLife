using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenu : MonoBehaviour
{

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
