using System.ComponentModel;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [Tooltip("������")]
    [SerializeField] private EconomyController _economyController;

    [Tooltip("������ ������, ���� �������� �����")]
    [SerializeField] private GameObject _towerGroup;
    [Tooltip("������ �����")]
    [SerializeField] private GameObject _towerPrefab;
    [Tooltip("��������� ��������� �����")]
    [SerializeField] private int _buildCost;

    private ScriptableListScript _towerObjectListSO;
    private TowerScriptable _towerSO;
    private GameObject _buildPointObject;
    private Transform _buildPointTransform;

    public GameObject BuildPointObject { get => _buildPointObject; set => _buildPointObject = value; }

    private void Start()
    {
        _economyController.CurrentCost = _buildCost;
        _towerObjectListSO = Resources.Load<ScriptableListScript>("Tower/TowerObjects_SO");
    }

    public void BuildTower(TowerEnum towerEnum)
    {
        _buildPointTransform = _buildPointObject.transform;

        _towerSO = _towerObjectListSO.SOList.Find(item => item.TowerEnum == towerEnum);

        Vector3 position = _buildPointTransform.position;
        GameObject towerGO = GameObject.Instantiate(_towerSO.TowerPrefab, position, Quaternion.identity, _towerGroup.transform);
        towerGO.GetComponent<TowerAttak>().TowerSO = _towerSO;

        Destroy(_buildPointObject);
        TakeMoney();
    }

    private void TakeMoney()
    {
        _economyController.CurrentCost = _towerSO.TowerCost;
        _economyController.SpendCurrency();
    }

}
