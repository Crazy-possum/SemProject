using UnityEngine;

[CreateAssetMenu(fileName = "New TowerScriptable", menuName = "TowerSO")]

public class TowerScriptable : ScriptableObject
{
    public string Name;
    public string Description;
    public TowerEnum TowerEnum;
    public TowerTargetEnum TowerAtkPattern;
    public GameObject TowerPrefab;
    public Sprite TowerSprite;
    public int TowerCost;
}
