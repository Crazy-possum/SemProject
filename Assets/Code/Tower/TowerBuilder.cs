using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private ScriptableListScript _towerObjectListSO;
    private TowerSO _towerSO;
    private GameObject _buildPointObject;
    private Transform _buildPointTransform;

    private static Action<GameObject> _onBuildTower;

    private GameObject _buildedTower;
    private bool _isTowerBuilded;

    private int _towerAmount;

    public GameObject BuildPointObject { get => _buildPointObject; set => _buildPointObject = value; }
    public static Action<GameObject> OnBuildTower { get => _onBuildTower; set => _onBuildTower = value; }

    private void Start()
    {
        _economyController.CurrentCost = _buildCost;
        _towerObjectListSO = Resources.Load<ScriptableListScript>("Tower/TowerObjects_SO");
    }

    private void FixedUpdate()
    {
        if (_isTowerBuilded)
        {
            _onBuildTower?.Invoke(_buildedTower);
            _isTowerBuilded = false;
        }

        if (_towerAmount == 3 && _economyController.GeneralCurrency >= 20)
        {
            if (SceneManager.GetActiveScene().buildIndex == 3 &&
                PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FinishFirstWave}" &&
                PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
                PlayerPrefs.GetString($"{TutorConstantMaganer.CAN_UPGRADE}") == false.ToString())
            {
                TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.CAN_UPGRADE}");
                TutorController.OnTutorActive?.Invoke();
            }
        }
    }

    public void BuildTower(TowerEnum towerEnum)
    { 
        _buildPointTransform = _buildPointObject.transform;
        _towerSO = _towerObjectListSO.TowerSOList.Find(item => item.TowerEnum == towerEnum);
        Vector3 position = _buildPointTransform.position;
        GameObject towerGO = GameObject.Instantiate(_towerSO.TowerPrefab, position, Quaternion.identity, _towerGroup.transform);
        towerGO.GetComponent<TowerAttak>().TowerSO = _towerSO;

        _isTowerBuilded = true;
        _buildedTower = towerGO;

        Destroy(_buildPointObject);

        TakeMoney();
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            _towerAmount++;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyDie}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.SECOND_TOWER_BUILD}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SECOND_TOWER_BUILD}");
            TutorController.OnTutorActive?.Invoke();
        }
    }

    private void TakeMoney()
    {
        _economyController.CurrentCost = _towerSO.TowerCost;
        _economyController.SpendCurrency();
    }

}
