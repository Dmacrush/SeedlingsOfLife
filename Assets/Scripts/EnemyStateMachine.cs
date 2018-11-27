﻿using System.Collections;
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

	//tiemforactionstuff
	private bool actionStarted = false;

	//Reference to the targetted hero (enemy target -----> hero)
	public GameObject HeroToAttack;
	//Animation speed
	private float animSpeed = 5f;




	// Use this for initialization
	void Start()
	{
		//Set the current state to PROCESSING. 
		currentState = TurnState.PROCESSING;
		//Find the battle manager then get the battle state machine component
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
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

			case (TurnState.ACTION):
				StartCoroutine(TimeforAction());
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
		myAttack.Type = "Enemy";
		//Set the attacker gameobject to this gameObject
		myAttack.AttackersGameObject = this.gameObject;
		//Get a random enemy target in a range from the list and choose a random action
		myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
		//Set the Battle state machine to use myAttack
		BSM.CollectActions(myAttack);

	}

	private IEnumerator TimeforAction()
	{
		//Check if an action has already started
		if (actionStarted)
		{
			//Stop the coroutine here
			yield break;
		}
		//Otherwise set actionStarted bool to true
		actionStarted = true;
		//animate the enemy near the hero to attack the targetted hero, increase the targets.X position
		Vector3 targetHeroPosition = new Vector3 (HeroToAttack.transform.position.x-1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
		while (MoveTowardsEnemy(targetHeroPosition))
		{
			yield return null;
		}
		//wait for an amount of time
		yield return new WaitForSeconds(0.5f);
		//Do damage

		//animate back to the start position
		Vector3 initialPosition = startPosition;
		while (MoveTowardsInitialPosition(initialPosition))
		{
			yield return null;
		}
		//remove the performer from the list in the Battle State Machine so the next enemy can attack
		BSM.PerformList.RemoveAt(0);
		//Reset the battle state machine -> Wait
		BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

		actionStarted = false;
		//reset the enemy state
		currentCoolDown = 0;
		//set the current state to turn state.PROCESSING
		currentState = TurnState.PROCESSING;
	}

	private bool MoveTowardsEnemy(Vector3 target)
	{
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}
	private bool MoveTowardsInitialPosition(Vector3 target)
	{
		return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
	}
}

