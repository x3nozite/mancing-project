using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class ItemSplitter : MonoBehaviour
{
    public InventoryItemDescription parentPrefab;
    // public Slider slider;
    public Button confirmButton;
    public TextMeshProUGUI sliderValueText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // slider.minValue = 1;
        // slider.maxValue = parentPrefab.item.quantity - 1;
    }

    // Update is called once per frame
    void Update()
    {
        // sliderValueText.text = slider.value;
    }

    void ConfirmButtonClick()
    {
        // int newItemStack = slider.value;
    }
}
