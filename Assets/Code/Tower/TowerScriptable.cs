using UnityEngine;

[CreateAssetMenu(fileName = "New TowerScriptable", menuName = "TowerSO")]

public class TowerScriptable : ScriptableObject
{
    public string Name;
    public string Description;
    public TowerEnum TowerEnum;
    public GameObject TowerPrefab;
    public GameObject BulletPrefab;
    public Sprite TowerSprite;
    public float TowerReloadTime;
    public float BulletDuration;
    public float BulletDOTTime;
    public int MaxBulletDistance;
    public int TowerDamage;
    public int TowerRange;
    public int TowerCost;
}
