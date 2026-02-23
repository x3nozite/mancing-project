using UnityEngine;

public class PopUpTriggerZone : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private Collider colliderComponent;

    public void OnTriggerEnter2D()
    {
        PopUpMenuManager.Instance.OpenOverlayPopUpMenu(popUp);
    }

    public void OnTriggerExit2D()
    {
        PopUpMenuManager.Instance.CloseOverlayPopUpMenu(popUp);
    }
}
