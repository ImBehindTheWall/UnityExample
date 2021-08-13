using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Cotents")]
    public Item content;
    public bool isOpen;
    public BoolValue storedOpen;
    public Inventory playerInventory;
   
    [Header("Signals and dialogs")]
    public SignalSender raiseItem;
    public GameObject dialogWindow;
    public Text dialogText;


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            animator.SetBool("Opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.E) && dialogActive )
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogWindow.SetActive(true);
        dialogText.text = content.itemDiscription;
        playerInventory.AddItem(content);
        playerInventory.currentItem = content;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        animator.SetBool("Opened", true);
        storedOpen.RuntimeValue = isOpen;

    }

    public void ChestAlreadyOpen()
    {
        
            dialogWindow.SetActive(false);
            raiseItem.Raise();
        dialogActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            dialogActive = true;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            dialogActive = false;
        }
    }
}
