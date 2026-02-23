using System.Collections.Generic;
using UnityEngine;

public class PopUpMenuManager : MonoBehaviour
{
    public static PopUpMenuManager Instance;
    private GameObject currentPrimaryPopUp;
    private Stack<GameObject> modals = new Stack<GameObject>();
    [SerializeField] private Transform PopUpRoot;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPopUpRoot(Transform newRoot)
    {
        PopUpRoot = newRoot;
    }

    public void OpenPrimaryPopUpMenu(GameObject menu)
    {
        if(currentPrimaryPopUp != null)
        {
            ClosePrimaryPopUpMenu(currentPrimaryPopUp);
        }
        currentPrimaryPopUp = Instantiate(menu, PopUpRoot);
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
        GameObject newModal = Instantiate(modal, PopUpRoot);
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
        Instantiate(menu, PopUpRoot);
    }

    public void CloseOverlayPopUpMenu(GameObject menu)
    {
        Destroy(menu);
    }
}
