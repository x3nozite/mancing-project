using UnityEngine;

public class PersistentUIScript : MonoBehaviour
{
    public static PersistentUIScript instance;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject PopUpRoot;

    public Canvas getCanvas => canvas;
    public GameObject getPopUpRoot => PopUpRoot;
    public void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
