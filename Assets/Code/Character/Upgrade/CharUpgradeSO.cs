using UnityEngine;

[CreateAssetMenu(fileName = "CharUp", menuName = "CharUpgradeSO")]
public class CharUpgradeSO : ScriptableObject
{
    [Tooltip("�������� ���������")]
    public string Name;
    [Tooltip("��������")]
    public string Description;
    public CharacterUpgradeEnum UpgradeEnum;
    //TODO : Лучше назвать не Rare, а Rarity
    public CharUpgradeRareEnum CharUpgradeRare;
    [Tooltip("������ �� ������")]
    public Sprite UpgradeSprite;
    public int UpgradeIntValue;
    public float UpgradeFloatValue;
    public float AddUpgradeFloatValue;
    public int UpgradeIndex;
}
