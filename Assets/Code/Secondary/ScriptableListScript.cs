using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableList", menuName = "SOList")]

public class ScriptableListScript : ScriptableObject
{
    public List<TowerScriptable> TowerSOList;
    public List<TowerUpgradeSO> TowerUpgradeSOList;
    public List<CharUpgradeRareSO> CharUpgradeRareSOList;
}
