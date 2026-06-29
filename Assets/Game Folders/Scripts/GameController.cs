using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Bomb bombPrefab;

    public event Action<SwipeData> OnSwiped;
    public event Action<float> OnCannonMoved;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    public void Swipe(SwipeData data)
    {
        OnSwiped?.Invoke(data);
    }

    public void MoveCannon(float deltaX)
    {
        OnCannonMoved?.Invoke(deltaX);
    }
}


[System.Serializable]
public class SwipeData
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float distance;    // 0.0 - 1.0 (normalized power)
    public float normalizedX; // -1.0 kiri, +1.0 kanan
}