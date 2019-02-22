using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public int CurrentLevel = 1;


    public void Start()
    {
        
        if (PlayerPrefs.GetInt("Level Completed") > 1)
        {
            CurrentLevel = PlayerPrefs.GetInt("Level Completed");
        }
        else
        {
            CurrentLevel = 1;
        }
    }
    public void Update()
    {
        if (CurrentLevel >= 4)
        {
            CurrentLevel = 1;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CurrentLevel += 1;
            SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Level Completed", CurrentLevel);
    }

}
