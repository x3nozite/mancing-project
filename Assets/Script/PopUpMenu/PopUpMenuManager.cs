using UnityEngine;

public class PopUpMenuManager : MonoBehaviour
{
    public static PopUpMenuManager Instance;
    private GameObject currentPrimaryPopUp;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenPrimaryPopUpMenu(GameObject menuPrefab)
    {
        if(currentPrimaryPopUp != null)
        {
            ClosePrimaryPopUpMenu(currentPrimaryPopUp);
        }
        Canvas canvas = FindFirstObjectByType<Canvas>();
        currentPrimaryPopUp = Instantiate(menuPrefab, canvas.transform);
    }

    public void ClosePrimaryPopUpMenu(GameObject menu)
    {
        if (currentPrimaryPopUp != null)
        {
            Destroy(menu);
            currentPrimaryPopUp = null;
        }
    }
}
