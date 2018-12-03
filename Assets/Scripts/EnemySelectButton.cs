using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
	public GameObject enemyPrefab;

	public void SelectEnemy()
	{
		GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(enemyPrefab);//save enemy input prefab
	}
}
