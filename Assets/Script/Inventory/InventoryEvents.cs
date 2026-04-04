using System;
using UnityEngine;

public class InventoryEvents: MonoBehaviour
{
    public static InventoryEvents instance;
    public Action<ItemSlotScript, ItemSlotScript> OnItemDropped;

    public void Awake()
    {
        instance = this;        
    }
}
