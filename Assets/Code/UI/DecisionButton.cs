using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private TowerBuilder _towerBuilder;
    [Tooltip("Скрипт")]
    [SerializeField] private TowerUpgrader _towerUpgrader;
    [Tooltip("Панель с выбором башни на постройку")]
    [SerializeField] private GameObject _towerBuildPanel;
    [SerializeField] private Image _towerImage;
    [SerializeField] private TMP_Text _towerNameText;
    [SerializeField] private TMP_Text _towerCostText;
    [SerializeField] private TMP_Text _towerDescriptionText;
    [Tooltip("Скрипт")]
    [SerializeField] private TowerEnum _localTowerEnum;

    private Button _button;

    public TowerBuilder TowerBuilder { get => _towerBuilder; set => _towerBuilder = value; }
    public TowerUpgrader TowerUpgrader { get => _towerUpgrader; set => _towerUpgrader = value; }
    public GameObject TowerBuildPanel { get => _towerBuildPanel; set => _towerBuildPanel = value; }
    public TowerEnum LocalTowerEnum { get => _localTowerEnum; set => _localTowerEnum = value; }

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(BuildTower);
        _button.onClick.AddListener(UpgradeTower);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }


    public void CustomizationButton(TowerScriptable towerSO)
    {
        _towerImage.sprite = towerSO.TowerSprite;
        _towerNameText.text = towerSO.Name;
        _towerCostText.text = $"{ towerSO.TowerCost}";
        _towerDescriptionText.text = towerSO.Description;
    }

    private void BuildTower()
    {

        if (_localTowerEnum == TowerEnum.Cannon || _localTowerEnum == TowerEnum.Shotgun ||
            _localTowerEnum == TowerEnum.Catapult || _localTowerEnum == TowerEnum.Sniper)
        {
            _towerBuilder.BuildTower(_localTowerEnum);
            _towerBuildPanel.SetActive(false);
        }
    }

    private void UpgradeTower()
    {
        if (_localTowerEnum == TowerEnum.Cannon_firstUpgrade || _localTowerEnum == TowerEnum.Cannon_secordUpgrade || _localTowerEnum == TowerEnum.Cannon_thirdUpgrade ||
            _localTowerEnum == TowerEnum.Shotgun_firstUpgrade || _localTowerEnum == TowerEnum.Shotgun_secondUpgrade || _localTowerEnum == TowerEnum.Shotgun_thirdUpgrade ||
            _localTowerEnum == TowerEnum.Catapult_firstUpgrade || _localTowerEnum == TowerEnum.Catapult_secordUpgrade || _localTowerEnum == TowerEnum.Catapult_thirdUpgrade ||
            _localTowerEnum == TowerEnum.Sniper_firstUpgrade || _localTowerEnum == TowerEnum.Sniper_secordUpgrade || _localTowerEnum == TowerEnum.Sniper_thirdUpgrade)
        {
            _towerUpgrader.ChooseTowerUpgrade(_localTowerEnum);
            _towerBuildPanel.SetActive(false);
        }
    }
}
