using UnityEngine;

public class InventoryHotbar : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerItemManager playerItemManager;
    [SerializeField] HotbarSlot[] slots = new HotbarSlot[8];

    private int selectedIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshUI();
        changeSelecteditem(0);
        inventory.OnInventoryChanged += RefreshUI;
    }

    void RefreshUI()
    {
        int i = 0;
        foreach (HotbarSlot s in slots)
        {
            s.SetIndex(i, inventory);
            s.hotbar = this;
            s.SetSlotItem(inventory.items[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                changeSelecteditem();
            }
        }
    }

    void changeSelecteditem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetFocused(i == selectedIndex);
        }
        playerItemManager.SetEquippedItem(inventory.items[selectedIndex]);
    }

    public void changeSelecteditem(int newSelected)
    {
        selectedIndex = newSelected;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetFocused(i == selectedIndex);
        }
        playerItemManager.SetEquippedItem(inventory.items[selectedIndex]);
    }
}
