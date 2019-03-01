using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	//reference to the entire UI
	public GameObject inventoryUI;
	//transform reference to the parented object
	public Transform itemsParent;
	//reference to the array of slots
	private InventorySlot[] slots;
	//reference to the Inventory script
	private Inventory inventory;
    // Start is called before the first frame update
    void Start()
	{
		//set the inventory script to an instance
		inventory = Inventory.instance;
		//subscribe to an item change event, this event is triggered whenever an item is added or removed
		inventory.onItemChangedCallback += UpdateUI;
		//set the slots array equal to the items parent
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

    // Update is called once per frame
    void Update()
    {
		//check if the inventory key has been pressed which is "I" or "B"
		if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
    }
	//update the Inventory UI
	void UpdateUI()
	{
		//loop through each slot and check if there are more items to add
		for (int i = 0; i < slots.Length; i++)
		{
			//if i is less than the inventory item count
			if(i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
		//print in console
		Debug.Log("Updating UI");
	}

}
