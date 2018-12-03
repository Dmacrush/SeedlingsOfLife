using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
	private BattleStateMachine BSM;
	//Reference to the Base Hero Class
	public BaseHero heroStats;

	//Enum Declaration
	public enum TurnState
	{
		//State for when the bar is filling
		PROCESSING,

		//State for adding hero to a list
		ADDTOLIST,

		//State for waiting/Idle
		WAITING,

		//State for when the player is selecting an action
		SELECTING,

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
	//reference to the ProgressBar
	public Image theProgressBar;
	//Selector Reference
	public GameObject Selector;

	// Use this for initialization
	void Start()
	{
		//set the current cooldown to a random range to add randomness to the progress bars (can use luck stat to alter/speed stat
		currentCoolDown = Random.Range(0, 2.5f);
		//set the current selector to false
		Selector.SetActive(false);
		//Find the battle manager
		BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
		//Set the current state to PROCESSING. 
		currentState = TurnState.PROCESSING;
		

	}

	// Update is called once per frame
	void Update()
	{
		//Display current state in the console
		Debug.Log(currentState);

		switch (currentState)
		{
			case (TurnState.PROCESSING):
				UpdateProgressBar();

				break;

			case (TurnState.ADDTOLIST):
				//Add whichever hero is currently selected to the HerosToManage List.
				BSM.HerosToManage.Add(this.gameObject);
				//set the current state to turnstate.WAITING
				currentState = TurnState.WAITING;
				break;

			case (TurnState.WAITING):
				//idle state
				break;

			case (TurnState.SELECTING):

				break;

			case (TurnState.DEATH):

				break;
		}
	}

	void UpdateProgressBar()
	{
		//Add to the current cooldown based on the time that has past until it reaches the maximum cooldown time
		currentCoolDown = currentCoolDown + Time.deltaTime;
		//value for calculating the cooldown
		float calc_cooldown = currentCoolDown / maximumCoolDown;
		//clamp the x value between 0 and 1 of the cooldown value to represent the current progressbar
		theProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown,0,1),theProgressBar.transform.localScale.y,theProgressBar.transform.localScale.z);
		//check if the current cooldown is greater than or equal to the max cooldown then...change the current state to ADDTOLIST
		if(currentCoolDown >= maximumCoolDown)
		{
			currentState = TurnState.ADDTOLIST;
		}
	}
}
