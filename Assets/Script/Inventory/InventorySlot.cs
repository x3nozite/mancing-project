using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private int slotIndex;
    public Inventory inventory;

    public int getIndex()
    {
        return slotIndex;
    }
    public void SetIndex(int i)
    {
        slotIndex = i;
    }
}