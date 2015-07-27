using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : IItem {

    public int ItemID { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public Texture2D ItemIcon { get; set; }
    public int ItemPower { get; set; }
    public int ItemSpeed { get; set; }
    public ItemTypes ItemType;

    public enum ItemTypes
    {
        Weapon,
        Usable,
        Consumable,
        Head,
        Shoulder,
        Chest,
        Gloves,
        Legs,
        Feet,
        Ring,
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
