using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rt;
    private Vector3 originalPosition;
    private CanvasGroup cg;
    public Canvas canvas;
    public Transform originalParent;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalParent = transform.parent;
    }

    public void SetDraggable(bool i)
    {
        transform.SetParent(originalParent);
        cg.blocksRaycasts = true;
        rt.anchoredPosition = originalPosition;
        enabled = i;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rt.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);

        rt.anchoredPosition = originalPosition;
        cg.blocksRaycasts = true;
    }
}
