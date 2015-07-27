using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase: MonoBehaviour {

    public List<Item> Items = new List<Item>();

    void Start()
    {
        var chestTest = new ItemStats { Armor = 10, BaseDamage = 0, Dodge = 10, HitRating = 0, Resist = 10 };
        var weaponTest = new ItemStats { Armor = 0, BaseDamage = 10, Dodge = 5, HitRating = 10, Resist = 10 };

        Items.Add(new Item("Apple", 0, "Best Apple EU", Item.ItemTypes.Consumable));
        Items.Add(new Item("Shirt", 1, "Fancy Pants Shirt", Item.ItemTypes.Chest, chestTest));
        Items.Add(new Item("Sword", 2, "Bronze Sword of Doom", Item.ItemTypes.Weapon, weaponTest));
    }
}