using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{

	public enum PerformAction
	{
		//Waiting state(preparing for action)
		WAIT,
		//Make Action
		TAKEACTION,
		//Perform action
		PERFORMACTION
	}



	public PerformAction battleStates;

	public List<HandleTurns> PerformList = new List<HandleTurns>();
	public List<GameObject> HerosInBattle = new List<GameObject>();
	public List<GameObject> EnemiesInBattle = new List<GameObject>();

	//handles all the inputs of all the states
	public enum HeroGUI
	{
		ACTIVATE,
		WAITING,
		//Input 1 = Basic attack
		INPUT1,
		//Input 2 = Selected enemy
		INPUT2,
		DONE
	}

	public HeroGUI HeroInput;

	public List<GameObject> HerosToManage = new List<GameObject>();
	
	private HandleTurns HeroChoice;
	//The reference to the enemy button
	public GameObject enemyButton;
	//Spacer object transform
	public Transform Spacer;


	// Use this for initialization
	void Start ()
	{
		battleStates = PerformAction.WAIT;
		//Find current enemies in the game with the range of a list with the tag of enemy
		EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		//Find current enemies in the game with the range of a list with the tag of enemy
		HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
		//Call the Enemy button function
		EnemyButtons();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (battleStates)
		{
			//check if there is anyone in the list of performers
			case (PerformAction.WAIT):
				if (PerformList.Count > 0)
				{
					battleStates = PerformAction.TAKEACTION;
				}

				break;

			case (PerformAction.TAKEACTION):
				//Find the first game object in the list of attackers by name.
				GameObject performer = GameObject.Find(PerformList[0].Attacker);
				//Check if the attacker is of the type Enemy
				if (PerformList[0].Type == "Enemy")
				{
					//Get the Enemy state machine component and make it a variable
					EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
					//find the first target in the list of heros and set that as the target
					ESM.HeroToAttack = PerformList[0].AttackersTarget;
					//Set the enemy state machine state to take a action
					ESM.currentState = EnemyStateMachine.TurnState.ACTION;
				}
				//Check if the attacker is of the type Hero
				if (PerformList[0].Type == "Hero")
				{
					
					
				}
				battleStates = PerformAction.PERFORMACTION;
				break;

			case (PerformAction.PERFORMACTION):

			break;
		}
	}
	public void CollectActions(HandleTurns input)
	{
		PerformList.Add(input);
	}

	void EnemyButtons()
	{
		//for each enemy in the list of enemies in battle.
		foreach (GameObject enemy in EnemiesInBattle)
		{
			//Spawn the correct corresponding button to target the 
			GameObject newButton = Instantiate(enemyButton) as GameObject;
			//Get the enemy button select script and create a new instance of this
			EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

			EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();
			//create a buttonText variable find the text component in the game object
			Text buttonText = newButton.transform.Find("E1Text").gameObject.GetComponent<Text>();
			//find the selected enemys name and store it in the buttonText variable
			buttonText.text = cur_enemy.enemy.name;
			//Pass the content needed for when in battle
			button.enemyPrefab = enemy;

			//Set the button that is instantiated by grabbing the spacers transform
			newButton.transform.SetParent(Spacer, false);
		}
	}
}
