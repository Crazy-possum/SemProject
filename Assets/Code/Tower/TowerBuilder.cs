using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private EconomyController _economyController;

    [Tooltip("Пустой объект, куда спанятся башни")]
    [SerializeField] private GameObject _towerGroup;
    [Tooltip("Префаб башни")]
    [SerializeField] private GameObject _towerPrefab;
    [Tooltip("Стоимость постройки башни")]
    [SerializeField] private int _buildCost;

    private GameObject _buildPointObject;
    private Transform _buildPointTransform;

    public GameObject BuildPointObject { get => _buildPointObject; set => _buildPointObject = value; }

    private void Start()
    {
        _economyController.CurrentCost = _buildCost;
    }

    public void SpawnTower()
    {
        _buildPointTransform = _buildPointObject.transform;

        Vector3 position = _buildPointTransform.position;
        GameObject sceneGObject = GameObject.Instantiate(_towerPrefab, position, Quaternion.identity, _towerGroup.transform);

        Destroy(_buildPointObject);
        TakeMoney();
    }

    private void TakeMoney()
    {
        _economyController.SpendCurrency();
    }
}
