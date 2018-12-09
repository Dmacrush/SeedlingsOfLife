using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass 
{
	public string theName;
	
	public float baseHp;
	public float curHp;

	public float baseMp;
	public float curMp;

	public float baseATK;
	public float curATK;
	public float baseDEF;
	public float curDEF;
	//anything inheriting from this class can be added to the list
	public List<BaseAttack> attacks = new List<BaseAttack>();
}
