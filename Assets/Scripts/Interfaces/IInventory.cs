using UnityEngine;
using System.Collections;

public interface IInventory {
    
    //int SlotsX { get; set; }
    //int SlotsY { get; set; }
    //GUISkin Skin { get; set; }

    // Add an item to the inventory. Using the ItemID given in the Item Database
    void AddItem(int id);

    // Removes the first item given an ID from the inventory
    void RemoveItem(int id);

    // Does the inventory contain an item with the given ID?
    bool InventoryContains(int id);

    // Draw all the shit on the screen for the Inventory
    void DrawInventory();

    // Use a consumable item in the inventory
    void UseConsumable(Item item, int slot, bool deleteItem);

    // Create the tooltip on the GUI
    string CreateToolTip(Item item);

}
