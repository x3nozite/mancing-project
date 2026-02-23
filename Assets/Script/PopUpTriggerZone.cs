using UnityEngine;

public class PopUpTriggerZone : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private Collider2D colliderComponent;
    private GameObject activePopUp;

    public void OnTriggerEnter2D()
    {
        activePopUp = PopUpMenuManager.Instance.OpenOverlayPopUpMenu(popUp);
    }

    public void OnTriggerExit2D()
    {
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(activePopUp);
    }
}
