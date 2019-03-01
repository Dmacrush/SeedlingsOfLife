using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	//item variable to keep track of what item is in the slot
	private Item item;
	//reference to the image in the item slot
	public Image icon;

	//reference to the remove button
	public Button removeButton;

	//function that adds the interacted item to the item slot in the inventory
	public void AddItem(Item newItem)
	{
		//set the item reference to the new item 
		item = newItem;
		//set the icon sprite to the relevant item icon image
		icon.sprite = item.icon;
		//enable the icon image
		icon.enabled = true;
		//set the remove button to true
		removeButton.interactable = true;
	}
	//function to clear the item slot in the inventory
	public void ClearSlot()
	{
		//clear the item slot
		item = null;
		//set the icon sprite to null
		icon.sprite = null;
		//set the icon visibility to false
		icon.enabled = false;
		//set the remove button to false
		removeButton.interactable = false;
	}
	//function for when the remove button is pressed
	public void OnRemoveButton()
	{
		Inventory.instance.Remove(item);
	}
	//function that runs when you use an item
	public void UseItem()
	{
		//if there is an item in the inventory
		if (item != null)
		{
			item.Use();
		}
	}
}
