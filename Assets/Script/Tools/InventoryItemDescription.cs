using UnityEngine;
using TMPro;

public class InventoryItemDescription : MonoBehaviour
{
    public TextMeshProUGUI description;
    public ItemInstance item;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDescription(ItemInstance item)
    {
        this.item = item;
        description.text = item.item.description;
    }
}
