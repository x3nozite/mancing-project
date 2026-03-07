using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpMenuManager : MonoBehaviour
{
    public static PopUpMenuManager Instance;
    private GameObject currentPrimaryPopUp;
    private GameObject currentPrimaryPopUpPrefab;
    private Stack<GameObject> modals = new Stack<GameObject>();
    private List<GameObject> overlayPopUps = new List<GameObject>();
    [SerializeField] private Transform PopUpRoot;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Transform canvas = GameObject.FindGameObjectWithTag("PopUpRoot").transform;
        if (canvas != null) {
            SetPopUpRoot(canvas);
        }
    }

    public void SetPopUpRoot(Transform newRoot)
    {
        PopUpRoot = newRoot;
    }

    public GameObject OpenPrimaryPopUpMenu(GameObject menu)
    {
        if(currentPrimaryPopUp != null && currentPrimaryPopUpPrefab == menu)
        {
            return currentPrimaryPopUp;
        }
        else if(currentPrimaryPopUp != null)
        {
            ClosePrimaryPopUpMenu(currentPrimaryPopUp);
        }
        currentPrimaryPopUpPrefab = menu;
        currentPrimaryPopUp = Instantiate(menu, PopUpRoot);
        return currentPrimaryPopUp;
    }

    public GameObject OpenPrimaryPopUpMenu(GameObject menu, Canvas canvas)
    {
        if (currentPrimaryPopUp != null && currentPrimaryPopUpPrefab == menu)
        {
            return currentPrimaryPopUp;
        }
        else if (currentPrimaryPopUp != null)
        {
            ClosePrimaryPopUpMenu(currentPrimaryPopUp);
        }
        currentPrimaryPopUpPrefab = menu;
        currentPrimaryPopUp = Instantiate(menu, canvas.transform);
        return currentPrimaryPopUp;
    }

    public void ClosePrimaryPopUpMenu(GameObject menu)
    {
        if (currentPrimaryPopUp != null)
        {
            Destroy(menu);
            currentPrimaryPopUp = null;
        }
    }

    public GameObject OpenBlockingModal(GameObject modal)
    {
        GameObject newModal = Instantiate(modal, PopUpRoot);
        modals.Push(newModal);
        return newModal;
    }

    public GameObject OpenBlockingModal(GameObject modal, Canvas canvas)
    {
        GameObject newModal = Instantiate(modal, canvas.transform);
        modals.Push(newModal);
        return newModal;
    }

    public void CloseBlockingModal()
    {
        if(modals.Count > 0)
        {
            GameObject top = modals.Pop();
            Destroy(top);
        }
    }

    public GameObject OpenOverlayPopUpMenu(GameObject menu)
    {
        GameObject activePopUp = Instantiate(menu, PopUpRoot);
        overlayPopUps.Add(activePopUp);
        return activePopUp;
    }

    public GameObject OpenOverlayPopUpMenu(GameObject menu, Canvas canvas)
    {
        GameObject activePopUp = Instantiate(menu, canvas.transform);
        overlayPopUps.Add(activePopUp);
        return activePopUp;
    }

    public void CloseOverlayPopUpMenu(GameObject menu)
    {
        if (overlayPopUps.Contains(menu))
        {
            overlayPopUps.Remove(menu);
            Destroy(menu);
        }
    }
}
