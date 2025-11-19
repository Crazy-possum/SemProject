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
    [SerializeField] private CharacterUpgrader _characterUpgrader;
    [SerializeField] private EconomyController _economyController;
    [Tooltip("Панель с выбором башни на постройку")]
    [SerializeField] private GameObject _towerBuildPanel;
    [SerializeField] private GameObject _towerUpgradePanel;
    [SerializeField] private GameObject _charUpgraderPanel;
    [SerializeField] private GameObject _tower;
    [SerializeField] private Image _towerImage;
    [SerializeField] private TMP_Text _towerNameText;
    [SerializeField] private TMP_Text _towerCostText;
    [SerializeField] private TMP_Text _towerDescriptionText;
    [SerializeField] private GameObject _buttonLocker;
    [Tooltip("Скрипт")]
    [SerializeField] private TowerEnum _localTowerEnum;

    private Button _button;
    private TowerUpgradeSO _upgradeSO = null;
    private TowerScriptable _towerSO = null;
    private CharUpgradeSO _charUpgradeSO = null;
    private bool _isEnoughMoney;

    public TowerBuilder TowerBuilder { get => _towerBuilder; set => _towerBuilder = value; }
    public TowerUpgrader TowerUpgrader { get => _towerUpgrader; set => _towerUpgrader = value; }
    public CharacterUpgrader CharacterUpgrader1 { get => _characterUpgrader; set => _characterUpgrader = value; }
    public GameObject TowerBuildPanel { get => _towerBuildPanel; set => _towerBuildPanel = value; }
    public GameObject TowerUpgradePanel { get => _towerUpgradePanel; set => _towerUpgradePanel = value; }
    public GameObject Tower { get => _tower; set => _tower = value; }
    public TowerEnum LocalTowerEnum { get => _localTowerEnum; set => _localTowerEnum = value; }
    public EconomyController EconomyController { get => _economyController; set => _economyController = value; }
    public GameObject CharUpgradePanel { get => _charUpgraderPanel; set => _charUpgraderPanel = value; }

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(BuildTower);
        _button.onClick.AddListener(UpgradeTower);
        _button.onClick.AddListener(CharLevelUp);
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

    public void CustomizationCharacterButton(CharUpgradeSO charSO)
    {
        _towerImage.sprite = charSO.UpgradeSprite;
        _towerNameText.text = charSO.Name;
        _towerDescriptionText.text = charSO.Description;

        _charUpgradeSO = charSO;
    }

    private void BuildTower()
    {
        if (_isEnoughMoney)
        {
            if (_towerSO != null)
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
            if (_upgradeSO != null)
            {
                _towerUpgrader.SetTowerUpgrade(_tower, _upgradeSO);
                _towerUpgradePanel.SetActive(false);
            }
        }
    }

    private void CharLevelUp()
    {
        if (_charUpgradeSO != null)
        {
            _characterUpgrader.ApplyUpgrade(_charUpgradeSO);
            _charUpgraderPanel.SetActive(false);
        }
    }
}
