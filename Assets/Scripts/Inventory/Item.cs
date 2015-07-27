using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : IItem {

    public int ItemID { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public Texture2D ItemIcon { get; set; }
    public ItemTypes ItemType { get; set; }
    public ItemStats ItemStats { get; set; }

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

    public Item(string name, int id, string desc, ItemTypes type)
    {
        ItemName = name;
        ItemID = id;
        ItemDescription = desc;
        ItemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
        ItemType = type;
    }

    public Item(string name, int id, string desc, ItemTypes type, ItemStats itemStats)
    {
        ItemName = name;
        ItemID = id;
        ItemDescription = desc;
        ItemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
        ItemType = type;
        ItemStats = itemStats;
    }
}
