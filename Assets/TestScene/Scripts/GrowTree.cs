using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTree : MonoBehaviour
{
	//maximum tree growth value
	public float maxGrowth = 0.3f;

	//tree growth speed
	public float growthSpeed = 0.1f;

	//temp scale variable
	private Vector3 tempScale;
	
	// Update is called once per frame
	void Update()
	{

		//tempScale = new Vector3(1.0f,1.0f,1.0f);
		//if the local scale of the the tree is less than the maxGrowth value
		if (transform.localScale.x < maxGrowth)
		{
			//startGrowing = true; 
			//transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * growthSpeed;
			tempScale.x += Time.deltaTime * growthSpeed;
			tempScale.y += Time.deltaTime * growthSpeed;
			tempScale.z += Time.deltaTime * growthSpeed;
			//increase the local scale by the tempScale variable.
			transform.localScale = tempScale;

		}

		
	}
}

