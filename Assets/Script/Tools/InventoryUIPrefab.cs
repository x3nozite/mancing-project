using UnityEngine;

public class InventoryUIPrefab : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ItemInstance item in inventory.items)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
