﻿using UnityEngine;

public class ListOfItemUI : MonoBehaviour
{
    public GameObject listOfItemPanel;      //listOfItemPanel
    public Transform itemSlotContainer;     //allItemSlotContainer
    public GameObject itemSlotPrefab;       //allitemSlotPrefab
    public ItemDatabase itemDatabase;       //itemDatabase
    public Inventory playerInventory;       //playerInventory
    public InventoryUI inventoryUI;         //inventoryUI

    void Start()
    {
        ShowAllItemsInGame();
    }

    public void ShowAllItemsInGame()
    {
        //Destroy all existing Item Slot
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        //Create a new Item Slot for each item in the item database
        foreach (Item item in itemDatabase.allItems)
        {

            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            if (itemSlot == null)
            {
                Debug.LogError("itemSlot is null");
            }

            //Create a new Item Slot for each item in the item database
            all_ItemSlot slot = itemSlot.GetComponent<all_ItemSlot>();
            if (slot == null)
            {
                Debug.LogError("slot is null");
            }
            slot.inventory = inventoryUI;
            slot.Setup(item);

        }
    }

    public void toggleListOfitem()
    {
        listOfItemPanel.SetActive(!listOfItemPanel.activeSelf);
    }
}
