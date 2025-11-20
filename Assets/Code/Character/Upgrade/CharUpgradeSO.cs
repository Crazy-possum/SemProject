using UnityEngine;

[CreateAssetMenu(fileName = "CharUp", menuName = "CharUpgradeSO")]
public class CharUpgradeSO : ScriptableObject
{
    [Tooltip("Название улучшения")]
    public string Name;
    [Tooltip("Описание")]
    public string Description;
    public CharacterUpgradeEnum UpgradeEnum;
    public CharUpgradeRareEnum CharUpgradeRare;
    [Tooltip("Иконка на кнопке")]
    public Sprite UpgradeSprite;
    public int UpgradeIntValue;
    public float UpgradeFloatValue;
    public float AddUpgradeFloatValue;
    public int UpgradeIndex;
}
