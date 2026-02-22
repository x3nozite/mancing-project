using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuTextOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    private static Material textBase;
    private static Material textHoverHighlighted;
    public void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        textBase = text.fontSharedMaterial;

        textHoverHighlighted = new Material(textBase);
        textHoverHighlighted.SetFloat(ShaderUtilities.ID_GlowPower, 1.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontMaterial = textHoverHighlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontMaterial = textBase;
    }
}
