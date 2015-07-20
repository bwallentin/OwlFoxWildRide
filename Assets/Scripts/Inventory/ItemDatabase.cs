using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Apple", 0, "Best Apple EU", 0, 0, Item.ItemTypes.Consumable));
        items.Add(new Item("Shirt", 0, "Fancy Pants Shirt", 0, 0, Item.ItemTypes.Weapon));
        items.Add(new Item("Sword", 0, "Bronze Sword of Doom", 1, 2, Item.ItemTypes.Weapon));
    }
}
