using System;
using UnityEngine;

public class InventoryEvents: MonoBehaviour
{
    public static InventoryEvents instance;
    public Action<InventorySlot, InventorySlot> OnItemDropped;

    public void Awake()
    {
        instance = this;        
    }
}
