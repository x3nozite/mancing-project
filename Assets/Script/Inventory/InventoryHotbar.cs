using UnityEngine;

public class InventoryHotbar : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerItemManager playerItemManager;
    [SerializeField] HotbarSlot[] slots = new HotbarSlot[8];

    private int selectedItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 0;
        foreach(HotbarSlot s in slots)
        {
            s.SetItem(inventory.hotbarItems[i]);
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
                selectedItem = i;
                changeSelecteditem();
            }
        }
    }

    void changeSelecteditem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetFocused(i == selectedItem);
        }
        playerItemManager.SetEquippedItem(slots[selectedItem].item);
    }

    public void changeSelecteditem(int newSelected)
    {
        selectedItem = newSelected;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetFocused(i == selectedItem);
        }
        playerItemManager.SetEquippedItem(slots[selectedItem].item);
    }
}
