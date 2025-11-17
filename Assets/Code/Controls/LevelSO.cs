using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelSO", menuName = "LevelSO")]

public class LevelSO : ScriptableObject
{
    public string Name;
    public int Index;

    public List<SpawnPresetSO> WavePresetList;
}
