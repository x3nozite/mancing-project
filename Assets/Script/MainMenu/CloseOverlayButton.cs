using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseOverlayButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefabRoot;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PopUpMenuManager.Instance == null)
        {
            Debug.Log("Manager is null");
            return;
        }
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(prefabRoot);
    }
}
