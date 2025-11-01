using UnityEngine;

[CreateAssetMenu(fileName = "New TowerScriptable", menuName = "TowerSO")]

public class TowerScriptable : ScriptableObject
{
    [Tooltip("�������� �����")]
    public string Name;
    [Tooltip("��������")]
    public string Description;
    [Tooltip("��� �����")]
    public TowerEnum TowerEnum;
    [Tooltip("������ �����")]
    public GameObject TowerPrefab;
    [Tooltip("������ �������")]
    public GameObject BulletPrefab;
    [Tooltip("������ �� ������")]
    public Sprite TowerSprite;
    [Tooltip("����� �����������")]
    public float TowerReloadTime;
    [Tooltip("������������ �������� ������� �������")]
    public float BulletDuration;
    [Tooltip("����� �������� ������� �������")]
    public float BulletDOTTime;
    [Tooltip("���� ������ �����")]
    public float AttakeAngle;
    [Tooltip("��������� ������������� �������")]
    public int MaxBulletDistance;
    [Tooltip("���������� ��������")]
    public int BulletAmount;
    [Tooltip("���� �����")]
    public int TowerDamage;
    [Tooltip("������ �����")]
    public int TowerRange;
    [Tooltip("��������� ���������")]
    public int TowerCost;
}
