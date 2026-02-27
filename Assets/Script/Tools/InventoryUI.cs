using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    
    [SerializeField] GameObject inventoryPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PopUpMenuManager.Instance.OpenPrimaryPopUpMenu(inventoryPrefab);
        }
    }
}
