using UnityEngine;

[CreateAssetMenu(fileName = "New TowerScriptable", menuName = "TowerSO")]

public class TowerSO : ScriptableObject
{
    [Tooltip("Название башни")]
    public string Name;
    [Tooltip("Описание")]
    public string Description;
    [Tooltip("Тип башни")]
    public TowerEnum TowerEnum;
    [Tooltip("Префаб башни")]
    public GameObject TowerPrefab;
    [Tooltip("Префаб патрона")]
    public GameObject BulletPrefab;
    [Tooltip("Иконка на кнопке")]
    public Sprite TowerSprite;
    [Tooltip("Время перезарядки")]
    public float TowerReloadTime;
    [Tooltip("Длительность действия эффекта патрона")]
    public float BulletDuration;
    [Tooltip("Время действия эффекта патрона")]
    public float BulletDOTTime;
    [Tooltip("Угол конуса атаки")]
    public float AttakeAngle;
    [Tooltip("Радиус атаки")]
    public float TowerRange;
    [Tooltip("Дистанция существования патрона")]
    public int MaxBulletDistance;
    [Tooltip("Количество патронов")]
    public int BulletAmount;
    [Tooltip("Урон башни")]
    public int TowerDamage;
    [Tooltip("Стоимость постройки")]
    public int TowerCost;
}
