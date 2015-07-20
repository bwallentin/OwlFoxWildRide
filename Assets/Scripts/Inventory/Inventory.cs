using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

    public int slotsX, slotsY;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();

    private bool showInventory;
    private ItemDatabase database;
    private bool showToolTip = false;
    private string toolTip;
    private bool draggingItem = false;
    private Item draggedItem;
    private int prevIndex;

	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();

        // Add all items we have to the inventory
        foreach (var x in database.items)
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
    /// Add an item to the inventory
    /// </summary>
    /// <param name="id"></param>
    void AddItem(int id)
    {
        //TODO: Snygga till den här koden
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].ItemID == id)
                    {
                        inventory[i] = database.items[j];
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
    void RemoveItem(int id)
    {
        var item = inventory.FirstOrDefault(s => s.ItemID == id);

        if (item != null)
        {
            if (item.ItemID != null)
                inventory[item.ItemID] = new Item();
        }
    }

    /// <summary>
    /// Does the inventory contain an item with the given ID?
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool InventoryContains(int id)
    {
        return inventory.Any(x => x.ItemID == id);
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;

        if (showInventory)
        {
            DrawInventory();

            if (showToolTip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), toolTip, skin.GetStyle("Tooltip"));
        }

        if(draggingItem)
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.ItemIcon);
    }

    /// <summary>
    /// Draw all the shit on the screen for the Inventory
    /// </summary>
    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
                GUI.Box(slotRect, "", skin.GetStyle("Slot"));
                slots[i] = inventory[i];

                //ItemName will be null if it doesn't exist
                if (slots[i].ItemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].ItemIcon);

                    if (slotRect.Contains(e.mousePosition))
                    {
                        toolTip = CreateToolTip(slots[i]);
                        showToolTip = true;

                        // Item being dragged around in the inventory
                        if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = slots[i];
                            inventory[i] = new Item(); //Empty inventory slot
                        }

                        // Item is being dropped in the inventory
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                else 
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.mouseUp && draggingItem)
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

    string CreateToolTip(Item item)
    {
        toolTip = "<color=#4DA4BF>" + item.ItemName + "</color>\n\n" + "<color=#F2F2F2>" + item.ItemDescription + "</color>";
        return toolTip;
    }
}
