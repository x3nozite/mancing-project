using System.Collections.Generic;
using UnityEngine;

public class PopUpMenuManager : MonoBehaviour
{
    public static PopUpMenuManager Instance;
    private GameObject currentPrimaryPopUp;
    private Stack<GameObject> modals = new Stack<GameObject>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenPrimaryPopUpMenu(GameObject menu)
    {
        if(currentPrimaryPopUp != null)
        {
            ClosePrimaryPopUpMenu(currentPrimaryPopUp);
        }
        Canvas canvas = FindFirstObjectByType<Canvas>();
        currentPrimaryPopUp = Instantiate(menu, canvas.transform);
    }

    public void ClosePrimaryPopUpMenu(GameObject menu)
    {
        if (currentPrimaryPopUp != null)
        {
            Destroy(menu);
            currentPrimaryPopUp = null;
        }
    }

    public void OpenBlockingModal(GameObject modal)
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        GameObject newModal = Instantiate(modal, canvas.transform);
        modals.Push(newModal);
    }

    public void CloseBlockingModal()
    {
        if(modals.Count > 0)
        {
            GameObject top = modals.Pop();
            Destroy(top);
        }
    }

    public void OpenOverlayPopUpMenu(GameObject menu)
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        Instantiate(menu, canvas.transform);
    }

    public void CloseOverlayPopUpMenu(GameObject menu)
    {
        Destroy(menu);
    }
}
