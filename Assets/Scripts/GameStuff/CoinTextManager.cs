using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    public void Start()
    {
        coinDisplay.text = "" + playerInventory.coin;
    }
    public void UpdateCoinValue()
    {
        coinDisplay.text = "" + playerInventory.coin;
    }
}
