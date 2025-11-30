using System;
using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    private GameObject _towerObject;
    private TowerUpgradeSO _upgradeSO;
    private TowerView _view;
    private ScriptableListScript _towerUpgradeListSO;

    private static Action<float, GameObject> _onActivateCannonFirstUpgrade;
    private static Action<int, GameObject> _onActivateCannonSecondUpgrade;
    private static Action<GameObject> _onActivateCannonThirdUpgrade;
    private static Action<int, GameObject> _onActivateShotgunFirstUpgrade;
    private static Action<float, GameObject> _onActivateShotgunSecondUpgrade;
    private static Action<float, GameObject> _onActivateShotgunThirdUpgrade;
    private static Action<float, GameObject> _onActivateCatapultFirstUpgrade;
    private static Action<float, GameObject> _onActivateCatapultSecondUpgrade;
    private static Action<float, GameObject> _onActivateCatapultThirdUpgrade;
    private static Action<float, GameObject> _onActivateSniperFirstUpgrade;
    private static Action<float, float, int, GameObject> _onActivateSniperSecondUpgrade;
    private static Action<int, GameObject> _onActivateSniperThirdUpgrade;

    private int _intParametrUpgrade;
    private float _floatParametrUpgrade;
    private float _addFloatParametrUpgrade;

    public static Action<float, GameObject> OnActivateCannonFirstUpgrade { get => _onActivateCannonFirstUpgrade; set => _onActivateCannonFirstUpgrade = value; }
    public static Action<int, GameObject> OnActivateCannonSecondUpgrade { get => _onActivateCannonSecondUpgrade; set => _onActivateCannonSecondUpgrade = value; }
    public static Action<GameObject> OnActivateCannonThirdUpgrade { get => _onActivateCannonThirdUpgrade; set => _onActivateCannonThirdUpgrade = value; }
    public static Action<int, GameObject> OnActivateShotgunFirstUpgrade { get => _onActivateShotgunFirstUpgrade; set => _onActivateShotgunFirstUpgrade = value; }
    public static Action<float, GameObject> OnActivateShotgunSecondUpgrade { get => _onActivateShotgunSecondUpgrade; set => _onActivateShotgunSecondUpgrade = value; }
    public static Action<float, GameObject> OnActivateShotgunThirdUpgrade { get => _onActivateShotgunThirdUpgrade; set => _onActivateShotgunThirdUpgrade = value; }
    public static Action<float, GameObject> OnActivateCatapultFirstUpgrade { get => _onActivateCatapultFirstUpgrade; set => _onActivateCatapultFirstUpgrade = value; }
    public static Action<float, GameObject> OnActivateCatapultSecondUpgrade { get => _onActivateCatapultSecondUpgrade; set => _onActivateCatapultSecondUpgrade = value; }
    public static Action<float, GameObject> OnActivateCatapultThirdUpgrade { get => _onActivateCatapultThirdUpgrade; set => _onActivateCatapultThirdUpgrade = value; }
    public static Action<float, GameObject> OnActivateSniperFirstUpgrade { get => _onActivateSniperFirstUpgrade; set => _onActivateSniperFirstUpgrade = value; }
    public static Action<float, float, int, GameObject> OnActivateSniperSecondUpgrade { get => _onActivateSniperSecondUpgrade; set => _onActivateSniperSecondUpgrade = value; }
    public static Action<int, GameObject> OnActivateSniperThirdUpgrade { get => _onActivateSniperThirdUpgrade; set => _onActivateSniperThirdUpgrade = value; }

    private void Awake()
    {
        _towerUpgradeListSO = Resources.Load<ScriptableListScript>("Tower/Upgrades/TowerUpgrades_SO");
    }

    public void SetTowerUpgrade(GameObject tower, TowerUpgradeSO upgradeSO)
    {
        _upgradeSO = upgradeSO;
        _towerObject = tower;
        _intParametrUpgrade = upgradeSO.UpgradeIntValue;
        _floatParametrUpgrade = upgradeSO.UpgradeFloatValue;
        _view = tower.GetComponent<TowerView>();

        SetUpgradeSprite();
        ChooseTowerUpgrade();
    }

    private void SetUpgradeSprite()
    {
        switch(_upgradeSO.UpgradeIndex)
        {
            case 1: _view.FirstUpgradeSprite.SetActive(true); break;
            case 2: _view.SecondUpgradeSprite.SetActive(true); break;
            case 3: _view.ThirdUpgradeSprite.SetActive(true); break;
        }
    }

    private void ChooseTowerUpgrade()
    {
        if (_upgradeSO.TowerEnum == TowerEnum.Cannon_firstUpgrade)
        {
            ActivateCannonFirstUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Cannon_secondUpgrade)
        {
            ActivateCannonSecondUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Cannon_thirdUpgrade)
        {
            ActivateCannonThirdUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Shotgun_firstUpgrade)
        {
            ActivateShotgunFirstUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Shotgun_secondUpgrade)
        {
            ActivateShotgunSecondUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Shotgun_thirdUpgrade)
        {
            ActivateShotgunThirdUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Catapult_firstUpgrade)
        {
            ActivateCatapultFirstUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Catapult_secondUpgrade)
        {
            ActivateCatapultSecondUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Catapult_thirdUpgrade)
        {
            ActivateCatapultThirdUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Sniper_firstUpgrade)
        {
            ActivateSniperFirstUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Sniper_secondUpgrade)
        {
            ActivateSniperSecondUpgrade();
        }
        else if (_upgradeSO.TowerEnum == TowerEnum.Sniper_thirdUpgrade)
        {
            ActivateSniperThirdUpgrade();
        }
    }

    private void ActivateCannonFirstUpgrade()
    {
        float newTimerTime = _floatParametrUpgrade;

        _onActivateCannonFirstUpgrade?.Invoke(newTimerTime, _towerObject);
    }

    private void ActivateCannonSecondUpgrade()
    {
        int addDamage = _intParametrUpgrade;

        _onActivateCannonSecondUpgrade?.Invoke(addDamage, _towerObject);
    }

    private void ActivateCannonThirdUpgrade()
    {
        _onActivateCannonThirdUpgrade?.Invoke(_towerObject);
    }

    private void ActivateShotgunFirstUpgrade()
    {
        int addBulletAmount = _intParametrUpgrade;

        _onActivateShotgunFirstUpgrade?.Invoke(addBulletAmount, _towerObject);
    }

    private void ActivateShotgunSecondUpgrade()
    {
        float addTowerRange = _floatParametrUpgrade;

        _onActivateShotgunSecondUpgrade?.Invoke(addTowerRange, _towerObject);
    }

    private void ActivateShotgunThirdUpgrade()
    {
        float cutReload = _floatParametrUpgrade;

        _onActivateShotgunThirdUpgrade?.Invoke(cutReload, _towerObject);
    }

    private void ActivateCatapultFirstUpgrade()
    {
        float addAoeRange = _floatParametrUpgrade;

        _onActivateCatapultFirstUpgrade?.Invoke(addAoeRange, _towerObject);
    }

    private void ActivateCatapultSecondUpgrade()
    {
        float damageBonus = _floatParametrUpgrade;

        _onActivateCatapultSecondUpgrade?.Invoke(damageBonus, _towerObject);
    }

    private void ActivateCatapultThirdUpgrade()
    {
        float cutDotTriggeredTime = _floatParametrUpgrade;

        _onActivateCatapultThirdUpgrade?.Invoke(cutDotTriggeredTime, _towerObject);
    }

    private void ActivateSniperFirstUpgrade()
    {
        float cutReload = _floatParametrUpgrade;

        _onActivateSniperFirstUpgrade?.Invoke(cutReload, _towerObject);
    }

    private void ActivateSniperSecondUpgrade()
    {
        float dotDamage = _floatParametrUpgrade;
        float dotTimerTime = _addFloatParametrUpgrade;
        int dotCount = _intParametrUpgrade;

        _onActivateSniperSecondUpgrade?.Invoke(dotDamage, dotTimerTime, dotCount, _towerObject);
    }

    private void ActivateSniperThirdUpgrade()
    {
        int addDistance = _intParametrUpgrade;

        _onActivateSniperThirdUpgrade?.Invoke(addDistance, _towerObject);
    }
}
