using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tut_", menuName = "TutorStageSO")]

public class TutorStageSO : ScriptableObject
{
    public string Name;
    public TutorEnum TutorEnum;
    public List<TutorDialogSO> TutorDialogSOList;
}
