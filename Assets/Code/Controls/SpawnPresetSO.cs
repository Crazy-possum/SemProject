using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_", menuName = "SpawnPresetSO")]

public class SpawnPresetSO : ScriptableObject
{
    public List<GameObject> enemySequenceList;
    public float spawnTimerValue;
    public float waveStartTime;
}
