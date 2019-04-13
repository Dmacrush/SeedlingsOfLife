using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectsound;
    FMOD.Studio.EventInstance soundevent;


    public static GameManager instance;

    private PartyManager partyManager;

    public RegionData currentRegion;
    public GameObject playerCharacter;

    public Vector3 nextPlayerPosition;
    public Vector3 lastPlayerPosition; //before battle

    public string sceneToLoad;
    public string lastScene; //before battle

    public bool isWalking = false;
    public bool canEncounter = false;
    public bool gotAttacked = false;
    public bool canBattle = true;

    public int counter;

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

    private void Start()
    {
        soundevent = FMODUnity.RuntimeManager.CreateInstance(selectsound);
        
    }

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
        canBattle = true;
        partyManager = GetComponent<PartyManager>();
        
    }

    private void Update()
    {
        switch(gameState)
        {
            case (GameStates.WORLD_STATE):
                {
                    
                    if (isWalking && canBattle)
                    {
                        RandomEncounter();
                    }
                    if (gotAttacked)
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
        if (GameManager.instance.isWalking == false)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundevent, GetComponent<Transform>(), GetComponent<Rigidbody>());
            soundevent.start();
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
		
		//enemy reference here
		//amount of enemies
		enemyAmount = Random.Range(1, currentRegion.maxAmountEnemies + 1);
        //which enemies
        for(int i = 0; i < enemyAmount; i++)
        {
            if (counter <= partyManager.maxPartyMembers)
            {
                enemiesToBattle.Add(currentRegion.possibleEnemies[Random.Range(0, currentRegion.possibleEnemies.Count)]);
                counter++;
            }
            else if (counter > partyManager.maxPartyMembers)
            {
                break;
            }
            counter = 0;
        }

        //amount of players
        playerAmount = partyManager.maxPartyMembers;
        //which players
        for (int i = 0; i < playerAmount; i++)
        {
            if(counter <= partyManager.maxPartyMembers)
            {
                playersToBattle.Add(partyManager.seedlingsInParty[counter]);
                counter++;
            }
            else if(counter > partyManager.maxPartyMembers)
            {
                break;
            }
            counter = 0;
        }


        lastPlayerPosition = GameObject.Find("Player").gameObject.transform.position;
        nextPlayerPosition = lastPlayerPosition;
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentRegion.BattleScene);
        isWalking = false;
        gotAttacked = false;
        canEncounter = false;
    }

	[System.Serializable]
	public class SeedlingMoves
	{
		private string name;
		public MoveType category;
		public Stat moveStat;
		public BaseStats.SeedlingType moveType;
	}

	[System.Serializable]
	public class Stat
	{
		public float minimum;
		public float maximum;
	}

	public enum MoveType
	{
		Physical,
		Skill,
	}

    public IEnumerator BattleCoolDown()
    {
        canBattle = false;
        yield return new WaitForSeconds(3f);
        canBattle = true;
    }
}
