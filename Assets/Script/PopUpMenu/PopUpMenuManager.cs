using UnityEngine;

public class PopUpMenuManager : MonoBehaviour
{
    public static PopUpMenuManager Instance;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenPopUpMenu(GameObject menuPrefab)
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        Instantiate(menuPrefab, canvas.transform);
    }

    public void ClosePopUpMenu(GameObject menu)
    {
        Destroy(menu);
    }
}
