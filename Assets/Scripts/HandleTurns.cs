﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurns
{
	//whos turn is coming up?
	public string Attacker; //name of the attacker
	//Which object was attacking?
	public GameObject AttackersGameObject;
	//Who is going to be attacked?
	public GameObject AttackersTarget;


}