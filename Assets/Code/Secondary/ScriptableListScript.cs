using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableList", menuName = "SOList")]

public class ScriptableListScript : ScriptableObject
{
    public List<TowerScriptable> SOList;
}
