using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Loader:
                break;
            case GameState.Menu:
                break;
            case GameState.Setting:
                break;
            case GameState.Quit:
                break;
            case GameState.Game:
                break;
        }
    }

    private void OpenPage(PageName pageName)
    {

    }
}
