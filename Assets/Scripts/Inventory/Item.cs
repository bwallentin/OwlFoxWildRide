using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
    
    public string ItemName;
    public int ItemID;
    public string ItemDescription;
    public Texture2D ItemIcon;
    public int ItemPower;
    public int ItemSpeed;
    public ItemTypes ItemType;

    public enum ItemTypes
    {
        Weapon,
        Usable,
        Consumable,
        Chest,
        Head,
        Gloves,
        Legs,
        Shoes,
        Quest
    }

    public Item()
    {
        ItemID = -1;
    }

    public Item(string name, int id, string desc, int power, int speed, ItemTypes type)
    {
        ItemName = name;
        ItemID = id;
        ItemDescription = desc;
        ItemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
        ItemPower = power;
        ItemSpeed = speed;
        ItemType = type;
    }
}
