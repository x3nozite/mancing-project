using UnityEngine;
using UnityEngine.UI;

public class ItemSlotScript : MonoBehaviour
{
    private ItemInstance item;
    [SerializeField] private Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(ItemInstance item)
    {
        this.item = item;
        image.sprite = item.item.sprite;
    }
}
