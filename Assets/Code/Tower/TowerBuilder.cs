using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private EconomyController _economyController;

    [SerializeField] private GameObject _towerGroup;
    [SerializeField] private GameObject _towerObgect;

    public GameObject BuildPointObject;

    private Transform _buildPointTransform;

    private void Start()
    {
        _economyController.CurrentCost = 2;
    }

    public void SpawnTower()
    {
        _buildPointTransform = BuildPointObject.transform;

        Vector3 position = _buildPointTransform.position;
        GameObject sceneGObject = GameObject.Instantiate(_towerObgect, position, Quaternion.identity, _towerGroup.transform);

        Destroy(BuildPointObject);
        TakeMoney();

    }

    private void TakeMoney()
    {
        _economyController.SpendCurrency();
    }
}
