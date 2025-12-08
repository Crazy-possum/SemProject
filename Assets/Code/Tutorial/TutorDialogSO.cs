using UnityEngine;

[CreateAssetMenu(fileName = "Phase_", menuName = "TutorDialogSO")]

public class TutorDialogSO : ScriptableObject
{
    public bool IsCharacterSpeak;
    public string CharacterText;
    public string NPCText;
    public Sprite CharacterSprite;
    public Sprite NPCSprite;
}
