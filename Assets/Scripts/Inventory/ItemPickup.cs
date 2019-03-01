using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
	//reference to the item script
	public Item item;
	//calling the Interact method from the Interactable script and Over riding it.
	public override void Interact()
	{
		//calls the Interact method and executes
		base.Interact();

		PickUp();


	}

	void PickUp()
	{
		Debug.Log("Picking up" + item.name);
		//Add to our inventory
		bool wasPickedUp = Inventory.instance.Add(item);
		if (wasPickedUp)
		{
			//remove the game object from scene
			Destroy(gameObject);
		}
	}
}
