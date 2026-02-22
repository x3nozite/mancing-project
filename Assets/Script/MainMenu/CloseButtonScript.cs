using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseButtonScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject prefabRoot;
    public Image buttonImage;
    public Sprite normal;
    public Sprite pressed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PopUpMenuManager.Instance == null){
            Debug.Log("Manager is null");
            return;
        }
        PopUpMenuManager.Instance.ClosePrimaryPopUpMenu(prefabRoot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normal;
    }
}
