using UnityEngine;
/// <summary>
/// To access the functionality of this script simply, right click in the project window
/// Select create ---> Inventory ---> Item
/// This will create a scriptable object which contains all the relevant variables that you need. To add more
/// just put them in here.
/// </summary>
// The base item class. All items should derive from this. 

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

	new public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool isDefaultItem = false;      // Is the item default?

	// Called when the item is pressed in the inventory
	public virtual void Use()
	{
		// Use the item
		// Something might happen

		Debug.Log("Using " + name);
	}

	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this);
	}

}