using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RareSO", menuName = "CharUpgradeRareSO")]
public class CharUpgradeRareSO : ScriptableObject
{
    public CharUpgradeRareEnum CharUpgradeRare;
    public List<CharUpgradeSO> CharUpgradeSOList;
}
