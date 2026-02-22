using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGameScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.updateState(GameState.Gameplay);
        SceneManager.LoadScene("GameplayPlaceholder");
        Debug.Log("aaa");
    }
}
