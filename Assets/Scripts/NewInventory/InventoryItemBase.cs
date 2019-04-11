using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Debug = FMOD.Debug;


public enum EItemType
	{
		Default,
		Consumable,
		Tool
	}

public class InteractableItemBase : MonoBehaviour
{
	public string Name;

	public Sprite Image;

	public string InteractText = "Press Right Mouse Button to pick up the item";

	public EItemType ItemType;


	public virtual void OnInteract()
	{

	}

	public virtual bool CanInteract(Collider other)
	{
		
		HUD theHud = FindObjectOfType<HUD>();
		theHud.OpenMessagePanel(Name);
		return true;
	}
}

public class InventoryItemBase : InteractableItemBase
{
	public InventorySlots Slot { get; set; }


	public virtual void OnPickUp()
	{
		//TODO: Add logic what happens when you pick up player
		//set the gameobjects visibility to false
		Destroy(gameObject.GetComponent<Rigidbody>());
		gameObject.SetActive(false);
	}


	public virtual void OnDrop(InventoryItemBase item)
	{
		RaycastHit hit = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 1000))
		{
			gameObject.SetActive(true);
			gameObject.transform.position = hit.point;
			gameObject.transform.eulerAngles = DropRotation;
			//OnGrow(item);
		}
	}



	public virtual void OnUse()
	{
		transform.localPosition = PickPosition;
		transform.localEulerAngles = PickRotation;
	}

	public virtual void OnGrow(InventoryItemBase item)
	{
		if (item.GetComponent<GrowTree>())
		{
			GrowTree gt = FindObjectOfType<GrowTree>();
			gt.isPlanted = true;
			if (gt.isPlanted)
			{
				gt.GrowThePlant();
			}

			if (!gt.isPlanted)
			{
				gt.StopGrowingTree();
			}

		}
	}
	//if (item.GetComponent<GrowTree>())
	//{
	//	GrowTree gt = FindObjectOfType<GrowTree>();
	//gt.grow();
	//}



	/*public virtual void OnInteractAnimation(Animator animator)
	{
		animator.SetTrigger("tr_pickup");
	}*/

	public Vector3 PickPosition;
	public Vector3 PickRotation;
	public Vector3 DropRotation;

	public bool UseItemAfterPickup = false;
}



	


	