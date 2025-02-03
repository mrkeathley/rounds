using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour {
    public Inventory playerInventory;
    public TextMeshProUGUI coinText;

    public void UpdateCoinText() {
        coinText.text = playerInventory.coins.ToString();
    }
}