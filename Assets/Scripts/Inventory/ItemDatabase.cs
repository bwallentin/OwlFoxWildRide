using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase: MonoBehaviour {

    public List<Item> Items = new List<Item>();

    void Start()
    {
        Items.Add(new Item("Apple", 0, "Best Apple EU", 0, 0, Item.ItemTypes.Consumable));
        Items.Add(new Item("Shirt", 1, "Fancy Pants Shirt", 0, 0, Item.ItemTypes.Weapon));
        Items.Add(new Item("Sword", 2, "Bronze Sword of Doom", 1, 2, Item.ItemTypes.Weapon));
    }
}