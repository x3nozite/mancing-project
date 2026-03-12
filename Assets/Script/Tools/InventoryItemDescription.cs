using UnityEngine;
using TMPro;

public class InventoryItemDescription : MonoBehaviour
{
    public TextMeshProUGUI description;
    public TextMeshProUGUI itemName;
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
        itemName.text = item.item.name;
        itemName.color = item.item.RankColor; // 939393
    }
}
