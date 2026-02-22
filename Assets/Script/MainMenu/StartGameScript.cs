using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGameScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.updateState(GameState.Gameplay);
        SceneManager.LoadScene("BrandonScene");
    }
}
