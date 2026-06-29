using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameState currentState = GameState.Loader;

    public delegate void ChangeStateDelegate(GameState newState);
    public event ChangeStateDelegate OnStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ChangeState(GameState newState)
    {
        if(newState == currentState) { return; }
        currentState = newState;

        OnStateChanged?.Invoke(currentState);
    }
}
