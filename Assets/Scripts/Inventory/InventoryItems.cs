using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/items")]
public class InventoryItems : ScriptableObject
{
	public string itemName;
	public string itemDescription;
	public Sprite itemImage;
	public int numberHeld;
	public bool usable;
	public bool uniq;
	public UnityEvent thisEvent;
	public void Use()
	{
		Debug.Log("using item");
		thisEvent.Invoke();
	}

	public void DecreaseAmount()
	{
		numberHeld--;
		if (numberHeld < 0)
		{
			numberHeld = 0;
		}
	}
}
