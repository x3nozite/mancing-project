using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject optionMenu;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PopUpMenuManager.Instance == null) Debug.Log("Manager is null");
        if (optionMenu == null) Debug.Log("optionMenu prefab is null");
        PopUpMenuManager.Instance.OpenPrimaryPopUpMenu(optionMenu);
    }
}
