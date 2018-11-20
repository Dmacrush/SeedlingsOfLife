using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	// Use this for initialization
	void Start ()
	{
		battleStates = PerformAction.WAIT;
		//Find current enemies in the game with the range of a list with the tag of enemy
		EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		//Find current enemies in the game with the range of a list with the tag of enemy
		HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));

	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (battleStates)
		{
			case (PerformAction.WAIT):

			break;

			case (PerformAction.TAKEACTION):

			break;

			case (PerformAction.PERFORMACTION):

			break;
		}
	}
	public void CollectActions(HandleTurns input)
	{
		PerformList.Add(input);
	}
}
