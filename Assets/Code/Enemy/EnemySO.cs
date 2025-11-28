using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    public string Name;
    public EnemyEnum EnemyEnum;
    public GameObject EnemyPrefab;
    public float Speed;
    public float MaxHealth;
}
