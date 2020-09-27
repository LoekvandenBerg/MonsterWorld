using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> playerItems = new List<Item>();
    [SerializeField]
    private UIInventory inventoryUI;
    ItemDatabase itemDatabase;

    private void Awake()
    {
        if (FindObjectsOfType<Inventory>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }

    private void Start()
    {
        GiveItem(1);
        GiveItem(2);
        GiveItem(3);
        GiveItem(1);
        GiveItem(2);
        GiveItem(3);
        GiveItem(1);
        GiveItem(2);
        GiveItem(3);
        GiveItem(1);
        GiveItem(2);
        GiveItem(3);
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        //Debug.Log(itemToAdd.title);
        inventoryUI.AddItemToUI(itemToAdd);
        playerItems.Add(itemToAdd);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        inventoryUI.AddItemToUI(itemToAdd);
        playerItems.Add(itemToAdd);
    }

    public Item CheckForItem(int id)
    {
        return playerItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);

        if (itemToRemove != null)
        {
            playerItems.Remove(itemToRemove);
        }
    }
}
