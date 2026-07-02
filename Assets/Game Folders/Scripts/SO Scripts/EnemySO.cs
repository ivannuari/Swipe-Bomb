using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy",menuName = "New/Enemy")]
public class EnemySO : ScriptableObject
{
    public EnemyType enemyType;
    public string enemyName;
    public int enemyLevel;
    public int enemyHp;

    [Range(0.85f,3f)] public float enemySpeed;
    [Range(0.2f,5f)] public float enemyReactionTime;
}

public enum EnemyType
{
    Minion,
    Boss
}
