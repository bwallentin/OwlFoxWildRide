using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase: MonoBehaviour {

    public List<Item> Items = new List<Item>();

    void Start()
    {
        var chestTest = gameObject.AddComponent<ItemStats>();
        chestTest.Armor = 10;
        chestTest.BaseDamage = 0;
        chestTest.Dodge = 10;
        chestTest.HitRating = 0;
        chestTest.Resist = 10;

        var weaponTest = gameObject.AddComponent<ItemStats>();
        weaponTest.Armor = 0;
        weaponTest.BaseDamage = 10;
        weaponTest.Dodge = 0;
        weaponTest.HitRating = 10;
        weaponTest.Resist = 0;

        Items.Add(new Item("Apple", 0, "Best Apple EU", Item.ItemTypes.Consumable));
        Items.Add(new Item("Shirt", 1, "Fancy Pants Shirt", Item.ItemTypes.Chest, chestTest));
        Items.Add(new Item("Sword", 2, "Bronze Sword of Doom", Item.ItemTypes.Weapon, weaponTest));
    }
}