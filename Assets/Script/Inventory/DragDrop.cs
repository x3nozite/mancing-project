using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rt;
    private Vector3 originalPosition;

    private CanvasGroup cg;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
    }

    public void SetDraggable(bool i)
    {
        cg.blocksRaycasts = true;
        enabled = i;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rt.anchoredPosition;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.anchoredPosition += eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rt.anchoredPosition = originalPosition;
        cg.blocksRaycasts = true;
    }
}
