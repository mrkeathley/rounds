using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu]
public class Inventory : ScriptableObject {
    public Item currentItem;
    public List<Item> items = new();
    public int numberOfKeys;
    public int coins;

    public void AddItem(Item item) {
        if (item.isKey) {
            numberOfKeys++;
        }
        else {
            if (!items.Contains(item)) {
                items.Add(item);
            }
        }
    }
}