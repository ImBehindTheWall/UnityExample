using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worldpotion : MonoBehaviour
{
    [SerializeField] private playerInventory playerInventory;
    [SerializeField] private InventoryItems thisItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }

    }

    void AddItemToInventory()
    {
        if (playerInventory && this)
        {
            if (playerInventory.inventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
            }
            else
            {
             
                playerInventory.inventory.Add(thisItem);
                thisItem.numberHeld++;


            }
        }
    }
}
