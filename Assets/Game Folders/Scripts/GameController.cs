using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Bomb bombPrefab;

    [SerializeField] private Transform enemySpawnerPosition;
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private GameObject[] allFxs;

    private int wavesNumber = 0;

    public event Action<SwipeData> OnSwiped;
    public event Action<Vector2> OnCannonMoved;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < wavesNumber + 5; i++)
        {
            var e = Instantiate(enemyPrefab, enemySpawnerPosition.position, Quaternion.identity);
        }
    }

    public void Swipe(SwipeData data)
    {
        OnSwiped?.Invoke(data);
    }

    public void MoveCannon(Vector2 forceData)
    {
        OnCannonMoved?.Invoke(forceData);
    }

    public GameObject GetExplotionFx()
    {
        return allFxs[0];
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