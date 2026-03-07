using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> items = new List<ItemInstance>();
    [SerializeField] private ItemData placeholder;
    private ItemInstance placeholder_rod;
    [SerializeField] GameObject inventoryPrefab;
    private GameObject inventoryUIInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        placeholder_rod = new ItemInstance { item = placeholder};
        populate_placeholder();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUIInstance = PopUpMenuManager.Instance.OpenPrimaryPopUpMenu(inventoryPrefab);
            InventoryUIPrefab UIInventory = inventoryUIInstance.GetComponent<InventoryUIPrefab>();
            UIInventory.SetInventory(this);

        }
    }

    void populate_placeholder()
    {
        items.Add(placeholder_rod);
        items.Add(placeholder_rod);
        items.Add(placeholder_rod);
        items.Add(placeholder_rod);
    }
}
