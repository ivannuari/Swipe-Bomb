using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Bomb bombPrefab;

    [SerializeField] private Transform enemySpawnerPosition;
    [SerializeField] private Enemy[] enemyPrefab;

    [SerializeField] private GameObject[] allFxs;

    [SerializeField] private int wavesNumber = 0;
    [SerializeField] private int enemyCount = 0;

    public event Action<SwipeData> OnSwiped;
    public event Action<Vector2> OnCannonMoved;
    public event Action<int> OnWavesStarted;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    private void Start()
    {
        StartWaves();
    }

    private void StartWaves()
    {
        wavesNumber++;

        Enemy randEnemy = enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)];
        for (int i = 0; i < wavesNumber + 10; i++)
        {
            Enemy e = Instantiate(randEnemy, enemySpawnerPosition.position, Quaternion.identity);
        }

        OnWavesStarted?.Invoke(wavesNumber);
        StartCoroutine(NextWaves());
    }

    IEnumerator NextWaves()
    {
        yield return new WaitForSeconds(25f);
        StartWaves();
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

    public void EnemyDie()
    {
        enemyCount++;
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