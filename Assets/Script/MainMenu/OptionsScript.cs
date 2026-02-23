using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject optionMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        PopUpMenuManager.Instance.OpenPrimaryPopUpMenu(optionMenu);
    }
}
