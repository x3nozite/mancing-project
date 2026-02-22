using UnityEngine;
using UnityEngine.EventSystems;

public class CloseButtonScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefabRoot;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (PopUpMenuManager.Instance == null) Debug.Log("Manager is null");
        PopUpMenuManager.Instance.ClosePrimaryPopUpMenu(prefabRoot);
    }
}
