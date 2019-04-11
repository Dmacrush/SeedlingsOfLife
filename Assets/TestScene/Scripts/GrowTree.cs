using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Debug = UnityEngine.Debug;

public class GrowTree : MonoBehaviour
{

	/*public Vector3 grownScale;

	public Vector3 shrunkenScale;

	public float time;

	private bool active = false;

	private Vector3 originalScale;

	private float timePassed = 0.0f;

	private float progress;

	private enum State
	{
		SHRUNKEN,
		ORIGINAL_SIZE,
		GROWN
	}

	private State state = State.ORIGINAL_SIZE;

	private enum Actions
	{
		SHRINKING,
		NORMALIZING,
		GROWING
	}

	private Actions action = Actions.NORMALIZING;
	//maximum tree growth value
	//public float maxGrowth = 0.3f;

	//public Vector3 maxScale;
	//tree growth speed
	//public float growthSpeed = 0.1f;

	//public Vector3 currentGrowth;

	//temp scale variable
	//private Vector3 tempScale;

	//public bool isPlanted;

	private void FixedUpdate()
	{
		if (active)
		{
			timePassed += Time.deltaTime * 1000.0f;
			//0 - 1 with time
			progress = (timePassed / time);
		}
		switch (action)
		{
			case Actions.NORMALIZING:
				switch (state)
				{
					case State.SHRUNKEN:
						transform.localScale = new Vector3(
							(1 - progress) * shrunkenScale.x + progress * originalScale.x,
							(1 - progress) * shrunkenScale.y + progress * originalScale.y,
							(1 - progress) * shrunkenScale.z + progress * originalScale.z
						);
						break;
					case State.GROWN:
						transform.localScale = new Vector3(
							(1 - progress) * grownScale.x + progress * originalScale.x,
							(1 - progress) * grownScale.y + progress * originalScale.y,
							(1 - progress) * grownScale.z + progress * originalScale.z
						);
						break;
					default:
						break;
				}
				break;
			case Actions.GROWING:
				switch (state)
				{
					case State.SHRUNKEN:
						transform.localScale = new Vector3(
							(1 - progress) * shrunkenScale.x + progress * grownScale.x,
							(1 - progress) * shrunkenScale.y + progress * grownScale.y,
							(1 - progress) * shrunkenScale.z + progress * grownScale.z
						);
						break;
					case State.ORIGINAL_SIZE:
						transform.localScale = new Vector3(
							(1 - progress) * originalScale.x + progress * grownScale.x,
							(1 - progress) * originalScale.y + progress * grownScale.y,
							(1 - progress) * originalScale.z + progress * grownScale.z
						);
						break;
					default:
						break;
				}
				break;
			case Actions.SHRINKING:
				switch (state)
				{
					case State.ORIGINAL_SIZE:
						transform.localScale = new Vector3(
							(1 - progress) * originalScale.x + progress * shrunkenScale.x,
							(1 - progress) * originalScale.y + progress * shrunkenScale.y,
							(1 - progress) * originalScale.z + progress * shrunkenScale.z
						);
						break;
					case State.GROWN:
						transform.localScale = new Vector3(
							(1 - progress) * grownScale.x + progress * shrunkenScale.x,
							(1 - progress) * grownScale.y + progress * shrunkenScale.y,
							(1 - progress) * grownScale.z + progress * shrunkenScale.z
						);
						break;
					default:
						break;
				}
				break;
			default:
				break;
		}
		if (progress >= 1)
		{
			active = false;
			switch (action)
			{
				case Actions.GROWING:
					state = State.GROWN;
					break;
				case Actions.NORMALIZING:
					state = State.ORIGINAL_SIZE;
					break;
				case Actions.SHRINKING:
					state = State.SHRUNKEN;
					break;
			}
		}
	}

	public void grow()
	{
		active = true;
		action = Actions.GROWING;
		timePassed = 0.0f;
	}

	public void shrink()
	{
		active = true;
		action = Actions.SHRINKING;
		timePassed = 0.0f;
	}

	public void originalSize()
	{
		active = true;
		action = Actions.NORMALIZING;
		timePassed = 0.0f;
	}
}*/






	// Update is called once per frame
	//maximum tree growth value
	public float maxGrowth = 0.3f;

	public Vector3 maxScale;

	//tree growth speed
	public float growthSpeed = 0.1f;

	public float currentGrowth;

	//temp scale variable
	public Vector3 tempScale;

	public bool isPlanted;

	void Start()
	{
		maxScale = new Vector3(1, 1, 1);

	}

	void Update()
	{
		if (isPlanted)
		{
			GrowThePlant();
			
			
		}

		if (transform.localScale.x >= 1)
		{
			isPlanted = false;
			StopGrowingTree();
		}
	}







	public void GrowThePlant()
	{
		isPlanted = true;
		currentGrowth++;
		Debug.Log("Called on grow");

		//tempScale = new Vector3(1.0f,1.0f,1.0f);
		//if the local scale of the the tree is less than the maxGrowth value
		tempScale = transform.localScale;
		//transform.localScale = tempScale;
		//startGrowing = true; 
		//transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * growthSpeed;
		tempScale.x += Time.deltaTime * growthSpeed;
		tempScale.y += Time.deltaTime * growthSpeed;
		tempScale.z += Time.deltaTime * growthSpeed;
		//increase the local scale by the tempScale variable.
		transform.localScale = tempScale;
		
	}

	public void StopGrowingTree()
	{
		currentGrowth = 0;
		isPlanted = false;
		//transform.localScale = maxScale;
		//currentGrowth = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		tempScale = transform.localScale;
	}
}


