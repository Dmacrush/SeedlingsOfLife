using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
	private BattleStateMachine BSM;
    public BaseEnemy enemy;

	//Enum Declaration
	public enum TurnState
	{
		//State for when the bar is filling
		PROCESSING,

		//Choose an action to perform
		CHOOSEACTION,

		//State for adding hero to a list
		ADDTOLIST,

		//State for waiting/Idle
		WAITING,

		//State for when the player can make performs an action
		ACTION,

		//State for death
		DEATH,
	}

	//Enum reference
	public TurnState currentState;

	//For the Progressbar
	private float currentCoolDown = 0;
	private float maximumCoolDown = 5f;
	//This gameObject references
	private Vector3 startPosition;
	



	// Use this for initialization
	void Start ()
	{
		//Set the current state to PROCESSING. 
		currentState = TurnState.PROCESSING;
		//Find the battle manager then get the battle state machine component
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Display current state in the console
		//Debug.Log(currentState);

		switch (currentState)
		{
			case (TurnState.PROCESSING):
				UpdateProgressBar();
				break;

			case (TurnState.CHOOSEACTION):
				ChooseAction();
				currentState = TurnState.WAITING;

				break;
			case (TurnState.WAITING):

				break;

			case (TurnState.DEATH):

				break;
		}
	}
	void UpdateProgressBar()
	{
		//Add to the current cooldown based on the time that has past until it reaches the maximum cooldown time
		currentCoolDown = currentCoolDown + Time.deltaTime;
		//check if the current cooldown is greater than or equal to the max cooldown then...change the current state to ADDTOLIST
		if (currentCoolDown >= maximumCoolDown)
		{
			currentState = TurnState.CHOOSEACTION;
		}
	}

	void ChooseAction()
	{
		HandleTurns myAttack = new HandleTurns();
		//find the attacker and get their name
		myAttack.Attacker = enemy.name;
		//Set the attacker gameobject to this gameObject
		myAttack.AttackersGameObject = this.gameObject;
		//Get a random enemy target in a range from the list and choose a random action
		myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
		//
		BSM.CollectActions(myAttack);

	}
}

