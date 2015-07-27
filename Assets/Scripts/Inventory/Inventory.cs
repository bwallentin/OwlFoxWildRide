using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour, IInventory {

    public int rowCount;
    public int colCount;
    public GUISkin Skin;

    private List<Item> inventory;
    private List<Item> slots;
    private bool showInventory;
    private ItemDatabase database;
    private bool showToolTip = false;
    private string toolTip;
    private bool draggingItem = false;
    private Item draggedItem;
    private int draggedIndex;
    private GameObject player;

    private int slotSize = 50;

	// Use this for initialization
	void Start () 
    {
        inventory = new List<Item>();
        slots = new List<Item>();

        for (int i = 0; i < (rowCount * colCount); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        //showInventory = false;
        player = GameObject.Find("Player");
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();

        // Add all items we have to the inventory
        foreach (var x in database.Items)
            AddItem(x.ItemID);
	}

    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            // If false, set true. If true, set false.
            showInventory = !showInventory;
        }
    }

    /// <summary>
    /// Add an item to the inventory. Using the ItemID given in the Item Database
    /// </summary>
    /// <param name="id"></param>
    public void AddItem(int id)
    {
        //TODO: Snygga till den här koden
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < database.Items.Count; j++)
                {
                    if (database.Items[j].ItemID == id)
                    {
                        inventory[i] = database.Items[j];
                    }
                }
                break;
            }
        }
    }

    /// <summary>
    /// Removes the first item given an ID from the inventory
    /// </summary>
    /// <param name="id"></param>
    public void RemoveItem(int id)
    {
        var item = inventory.FirstOrDefault(s => s.ItemID == id);

        if (item != null)
            if (item.ItemID == -1)
                inventory[item.ItemID] = new Item();
    }

    /// <summary>
    /// Does the inventory contain an item with the given ID?
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool InventoryContains(int id)
    {
        return inventory.Any(x => x.ItemID == id);
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = Skin;

        if (showInventory)
        {
            DrawInventory();

            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), toolTip, Skin.GetStyle("Tooltip"));
        }

        if(draggingItem)
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.ItemIcon);
    }

    /// <summary>
    /// Draw all the shit on the screen for the Inventory
    /// </summary>
    public void DrawInventory()
    {
        Event currentEvent = Event.current;
        int i = 0;
        for (int y = 0; y < colCount; y++)
        {
            for (int x = 0; x < rowCount; x++)
            {
                Rect currentRectangle = new Rect(x * 60, y * 60, 50, 50);
                GUI.Box(currentRectangle, "", Skin.GetStyle("Slot"));
                slots[i] = inventory[i];
                Item item = slots[i];

                // Check to see if the slot has an item in it
                // We do this with itemName as all slots have an instance of item, just not any item information
                if (slots[i].ItemName != null)
                {
                    // If there is an item, let's draw the items icon within that slot
                    GUI.DrawTexture(currentRectangle, slots[i].ItemIcon);

                    // Now lets check to see if the mouses position is withing the items slot
                    if (currentRectangle.Contains(currentEvent.mousePosition))
                    {
                        // Create a tooltip for the item we are hovering over
                        toolTip = CreateToolTip(slots[i]);
                        showToolTip = true;

                        // Here we're going to check to see if the left-mouse-button was clicked on an item and then dragged 
                        // We also make sure we're not already dragging an item
                        if (currentEvent.button == 0 && currentEvent.type == EventType.mouseDrag && !draggingItem)
                        {
                            // If we start dragging, set dragging to true and set the dragged item to the item in that slot.
                            // Empty the slot we dragged from (making it an empty slot) and make sure we keep a record of the slot we dragged that item from.
                            draggingItem = true;
                            draggedItem = item;
                            inventory[i] = new Item(); //Empty inventory slot
                            draggedIndex = i;
                        }

                        // Now let's check to see if we released mouse button while dragging an item
                        if (currentEvent.type == EventType.mouseUp && draggingItem)
                        {
                            // If so, we'll make that slots item equal to the item we were dragging
                            // Also need to set the slot we dragged FROM to have the item in the slot we dropped the item on
                            // Then set that we are not dragging an item anymore and the dragged item to be empty
                            inventory[draggedIndex] = item;
                            inventory[i] = draggedItem; //Change location of the item
                            draggingItem = false;
                            draggedItem = null;
                        }

                        // If we right-click on the item we are hovering
                        if (currentEvent.isMouse && currentEvent.type == EventType.mouseDown && currentEvent.button == 1)
                        {
                            // Is the item an consumable?
                            if (item.ItemType == Item.ItemTypes.Consumable)
                            {
                                // Use dat consumable yo!
                                UseConsumable(item, i, true);
                            }
                        }
                    }
                }
                else
                {
                    // Item is being dropped in the inventory
                    if (currentRectangle.Contains(currentEvent.mousePosition))
                    {
                        if (currentEvent.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }

                // Hide tooltip
                if (toolTip == "")
                    showToolTip = false;
                
                i++;
            }
        }
    }

    // Use a consumable item in the inventory
    public void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.ItemID)
        {
            case 0:
                var playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.IncreaseHealth(20);
                break;
            case 1:
                break;
            case 2:
                break;
        }

        // Remove the item
        if (deleteItem)
            inventory[slot] = new Item();
    }

    /// <summary>
    /// Create the tooltip on the GUI, no shit captain obvious
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public string CreateToolTip(Item item)
    {
        toolTip = "<color=#4DA4BF>" + item.ItemName + "</color>\n\n" + "<color=#F2F2F2>" + item.ItemDescription + "</color>";
        return toolTip;
    }

    /// <summary>
    /// Used so the inventory is not resetted all the fking time. This needs some work so it's functioning properly
    /// </summary>
    void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
            PlayerPrefs.SetInt("Inventory " + i, inventory[i].ItemID);
    }

    /// <summary>
    /// Same shit as SaveInventory, fixing so the inventory doesn't reset all the time
    /// </summary>
    void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 1 ? database.Items[PlayerPrefs.GetInt("Inventory " + i)] : new Item();
    }
}
