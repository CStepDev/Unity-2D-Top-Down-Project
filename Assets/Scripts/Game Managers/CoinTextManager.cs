using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Text coinText;

    public void UpdateCoinCount()
    {
        coinText.text = playerInventory.numberOfCoins.ToString();
    }
}
