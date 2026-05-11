using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemDescription : MonoBehaviour
{
    public TextMeshProUGUI description;
    public TextMeshProUGUI itemName;
    public ItemInstance item;
    public Button splitStackButton;
    public Inventory sourceInventory;

    private int newStack = 5;

    private void Start()
    {
        splitStackButton.onClick.AddListener(() => SplitStack(newStack));
    }

    private void SplitStack(int newStackQuantity)
    {
        if (sourceInventory.items.IndexOf(null) == -1)
        {
            Debug.Log("inventory full");
            return;
        }

        newStackQuantity = Mathf.Min(newStackQuantity, item.quantity);
        if (item.quantity <= 0 || item.quantity == newStackQuantity) return;

        // make sure to delete the item instance from inventory if quantity reaches 0
        item.quantity -= newStackQuantity;
        Debug.Log("splitting");
        ItemInstance newItem = new ItemInstance { item = item.item, level = item.level, quantity = newStackQuantity };
        sourceInventory.AddItemToNewSlot(newItem);
        sourceInventory.OnInventoryChanged?.Invoke();
    }

    public void SetDescription(ItemInstance item, Inventory sourceInv)
    {
        this.item = item;
        sourceInventory = sourceInv;

        description.text = item.item.description;
        itemName.text = item.item.name;
        itemName.color = item.item.RankColor; // 939393
    }
}
