using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RegionData currentRegion;

    public GameObject playerCharacter;

    public Vector3 nextPlayerPosition;
    public Vector3 lastPlayerPosition; //before battle

    public string sceneToLoad;
    public string lastScene; //before battle

    public bool isWalking = false;
    public bool canEncounter = false;
    public bool gotAttacked = false;

    public enum GameStates
    {
        WORLD_STATE,
        BATTLE_STATE,
        IDLE
    }

    public int enemyAmount;

    public List<GameObject> enemiesToBattle = new List<GameObject>();

    public int playerAmount;

    public List<GameObject> playersToBattle = new List<GameObject>();

    public GameStates gameState;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        if(!GameObject.Find("Player"))
        {
            GameObject player = Instantiate(playerCharacter, nextPlayerPosition, Quaternion.identity) as GameObject;
            player.name = "Player";
        } 
    }

    private void Update()
    {
        switch(gameState)
        {
            case (GameStates.WORLD_STATE):
                {
                    if(isWalking)
                    {
                        RandomEncounter();
                    }
                    if(gotAttacked)
                    {
                        gameState = GameStates.BATTLE_STATE;
                    }
                    break;
                }
            case (GameStates.BATTLE_STATE):
                {
                    //Load Battle Scene
                    StartBattle();
                    gameState = GameStates.IDLE;
                    break;
                }
            case (GameStates.IDLE):
                {
                    break;
                }
            
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneAfterBattle()
    {
        SceneManager.LoadScene(lastScene);
    }

    void RandomEncounter()
    {
        if(isWalking && canEncounter)
        {
            //the 10 refers to the likely hood of an encounter
            if(Random.Range(0,1000) < 50)
            {
                Debug.Log("U GOT ATTACKED");
                gotAttacked = true;
            }
        }
    }

    void StartBattle()
    {
        //amount of enemies
        enemyAmount = Random.Range(1, currentRegion.maxAmountEnemies + 1);
        //which enemies
        for(int i = 0; i < enemyAmount; i++)
        {
            enemiesToBattle.Add(currentRegion.possibleEnemies[Random.Range(0, currentRegion.possibleEnemies.Count)]);
        }
        //amount of enemies
        playerAmount = Random.Range(1, currentRegion.maxAmountPlayers + 1);
        //which enemies
        for (int i = 0; i < playerAmount; i++)
        {
            playersToBattle.Add(currentRegion.possiblePlayers[Random.Range(0, currentRegion.possiblePlayers.Count)]);
        }


        lastPlayerPosition = GameObject.Find("Player").gameObject.transform.position;
        nextPlayerPosition = lastPlayerPosition;
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentRegion.BattleScene);
        isWalking = false;
        gotAttacked = false;
        canEncounter = false;
    }

}
