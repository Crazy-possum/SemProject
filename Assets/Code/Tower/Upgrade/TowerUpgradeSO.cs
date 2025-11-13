using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeScriptable", menuName = "UpgradeSO")]
public class TowerUpgradeSO : ScriptableObject
{
    [Tooltip("Название башни")]
    public string TowerName;
    [Tooltip("Название улучшения")]
    public string Name;
    [Tooltip("Описание")]
    public string Description;
    [Tooltip("Тип улучшения")]
    public TowerEnum TowerEnum;
    [Tooltip("Иконка на кнопке")]
    public Sprite TowerSprite;
    public int UpgradeIntValue;
    public float UpgradeFloatValue;
    public int UpgradeIndex;
    [Tooltip("Стоимость улучшения")]
    public int TowerCost;
}
