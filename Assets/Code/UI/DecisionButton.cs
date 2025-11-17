using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private TowerBuilder _towerBuilder;
    [Tooltip("Скрипт")]
    [SerializeField] private TowerUpgrader _towerUpgrader;
    [SerializeField] private EconomyController _economyController;
    [Tooltip("Панель с выбором башни на постройку")]
    [SerializeField] private GameObject _towerBuildPanel;
    [SerializeField] private GameObject _towerUpgradePanel;
    [SerializeField] private GameObject _tower;
    [SerializeField] private Image _towerImage;
    [SerializeField] private TMP_Text _towerNameText;
    [SerializeField] private TMP_Text _towerCostText;
    [SerializeField] private TMP_Text _towerDescriptionText;
    [SerializeField] private GameObject _buttonLocker;
    [Tooltip("Скрипт")]
    [SerializeField] private TowerEnum _localTowerEnum;

    private Button _button;
    private TowerUpgradeSO _upgradeSO;
    private TowerScriptable _towerSO;
    private bool _isEnoughMoney;

    public TowerBuilder TowerBuilder { get => _towerBuilder; set => _towerBuilder = value; }
    public TowerUpgrader TowerUpgrader { get => _towerUpgrader; set => _towerUpgrader = value; }
    public GameObject TowerBuildPanel { get => _towerBuildPanel; set => _towerBuildPanel = value; }
    public GameObject TowerUpgradePanel { get => _towerUpgradePanel; set => _towerUpgradePanel = value; }
    public GameObject Tower { get => _tower; set => _tower = value; }
    public TowerEnum LocalTowerEnum { get => _localTowerEnum; set => _localTowerEnum = value; }
    public EconomyController EconomyController { get => _economyController; set => _economyController = value; }

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(BuildTower);
        _button.onClick.AddListener(UpgradeTower);
    }

    private void FixedUpdate()
    {
        if (_towerSO != null)
        {
            _isEnoughMoney = _economyController.GeneralCurrency >= _towerSO.TowerCost;
        }
        else if (_upgradeSO != null)
        {
            _isEnoughMoney = _economyController.GeneralCurrency >= _upgradeSO.UpgradeCost;
        }

        if (!_isEnoughMoney)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void CustomizationBuildButton(TowerScriptable towerSO)
    {
        _towerImage.sprite = towerSO.TowerSprite;
        _towerNameText.text = towerSO.Name;
        _towerCostText.text = $"{ towerSO.TowerCost}";
        _towerDescriptionText.text = towerSO.Description;

        _towerSO = towerSO;
    }

    public void CustomizationUpgradeButton(TowerUpgradeSO upgradeSO)
    {
        _towerImage.sprite = upgradeSO.TowerSprite;
        _towerNameText.text = upgradeSO.Name;
        _towerCostText.text = $"{upgradeSO.UpgradeCost}";
        _towerDescriptionText.text = upgradeSO.Description;

        _upgradeSO = upgradeSO;
    }

    private void BuildTower()
    {
        if (_isEnoughMoney)
        {
            if (_localTowerEnum == TowerEnum.Cannon || _localTowerEnum == TowerEnum.Shotgun ||
            _localTowerEnum == TowerEnum.Catapult || _localTowerEnum == TowerEnum.Sniper)
            {
                _towerBuilder.BuildTower(_localTowerEnum);
                _towerBuildPanel.SetActive(false);
            }
        }
    }

    private void UpgradeTower()
    {
        if (_isEnoughMoney)
        {
            if (_localTowerEnum == TowerEnum.Cannon_firstUpgrade || _localTowerEnum == TowerEnum.Cannon_secondUpgrade || _localTowerEnum == TowerEnum.Cannon_thirdUpgrade ||
            _localTowerEnum == TowerEnum.Shotgun_firstUpgrade || _localTowerEnum == TowerEnum.Shotgun_secondUpgrade || _localTowerEnum == TowerEnum.Shotgun_thirdUpgrade ||
            _localTowerEnum == TowerEnum.Catapult_firstUpgrade || _localTowerEnum == TowerEnum.Catapult_secondUpgrade || _localTowerEnum == TowerEnum.Catapult_thirdUpgrade ||
            _localTowerEnum == TowerEnum.Sniper_firstUpgrade || _localTowerEnum == TowerEnum.Sniper_secondUpgrade || _localTowerEnum == TowerEnum.Sniper_thirdUpgrade)
            {
                _towerUpgrader.SetTowerUpgrade(_tower, _upgradeSO);
                _towerUpgradePanel.SetActive(false);
            }
        }
    }
}
