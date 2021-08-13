using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory/player Inventory")]
public class playerInventory : ScriptableObject
{
    public List<InventoryItems> inventory = new List<InventoryItems>();
}
