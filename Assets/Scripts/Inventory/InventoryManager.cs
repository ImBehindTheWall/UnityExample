using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("inventory Info")]
    public playerInventory playerInventory;
    [SerializeField] private GameObject InventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItems currentItem;
    public bool inventOpen;
    public GameObject InvPanel;
    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);

        }
    }

    public void MakeInventorySlot()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.inventory.Count; i++)
            {
                if (playerInventory.inventory[i].numberHeld > 0 || playerInventory.inventory[i].itemName == "bottle") 
                {
                    GameObject temp = Instantiate(InventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    inventorySlot newSlot = temp.GetComponent<inventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.inventory[i], this);

                    }
                }
             
            }

        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        
        MakeInventorySlot();
        SetTextAndButton("", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("inventory"))
        {
            inventOpen = !inventOpen;
            if (inventOpen)
            {
                InvPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                InvPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void OnDisable()
    {
        ClearInventorySlots();
    }

    public void SetDecriptionAndButton(string newDiscription,bool IsButtonUsable,InventoryItems newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDiscription;
        useButton.SetActive(IsButtonUsable);
    }

    public void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseButtonClicked()
    {
        if (currentItem)
        {
            currentItem.Use();
            ClearInventorySlots();
            MakeInventorySlot();
            SetTextAndButton("", false);
        }
    }
}
