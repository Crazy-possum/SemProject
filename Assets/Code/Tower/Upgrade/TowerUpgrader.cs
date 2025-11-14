using System;
using System.Threading.Tasks;
using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    private GameObject _towerObject;
    private TowerUpgradeSO _upgradeSO;
    private TowerView _view;
    private ScriptableListScript _towerUpgradeListSO;

    private static Action<float, GameObject> _onActivateCannonFirstUpgrade;
    private static Action<int, GameObject> _onActivateCannonSecondUpgrade;
    private static Action<int, GameObject> _onActivateCannonThirdUpgrade;
    private static Action<int, GameObject> _onActivateShotgunFirstUpgrade;
    private static Action<float, GameObject> _onActivateShotgunSecondUpgrade;
    private static Action<float, GameObject> _onActivateShotgunThirdUpgrade;
    private static Action<float, GameObject> _onActivateCatapultFirstUpgrade;
    private static Action<int, GameObject> _onActivateCatapultSecondUpgrade;
    private static Action<int, GameObject> _onActivateCatapultThirdUpgrade;
    private static Action<float, GameObject> _onActivateSniperFirstUpgrade;
    private static Action<int, GameObject> _onActivateSniperSecondUpgrade;
    private static Action<int, GameObject> _onActivateSniperThirdUpgrade;

    private int _intParametrUpgrade;
    private float _floatParametrUpgrade;

    public static Action<float, GameObject> OnActivateCannonFirstUpgrade { get => _onActivateCannonFirstUpgrade; set => _onActivateCannonFirstUpgrade = value; }
    public static Action<int, GameObject> OnActivateCannonSecondUpgrade { get => _onActivateCannonSecondUpgrade; set => _onActivateCannonSecondUpgrade = value; }
    public static Action<int, GameObject> OnActivateCannonThirdUpgrade { get => _onActivateCannonThirdUpgrade; set => _onActivateCannonThirdUpgrade = value; }
    public static Action<int, GameObject> OnActivateShotgunFirstUpgrade { get => _onActivateShotgunFirstUpgrade; set => _onActivateShotgunFirstUpgrade = value; }
    public static Action<float, GameObject> OnActivateShotgunSecondUpgrade { get => _onActivateShotgunSecondUpgrade; set => _onActivateShotgunSecondUpgrade = value; }
    public static Action<float, GameObject> OnActivateShotgunThirdUpgrade { get => _onActivateShotgunThirdUpgrade; set => _onActivateShotgunThirdUpgrade = value; }
    public static Action<float, GameObject> OnActivateCatapultFirstUpgrade { get => _onActivateCatapultFirstUpgrade; set => _onActivateCatapultFirstUpgrade = value; }
    public static Action<int, GameObject> OnActivateCatapultSecondUpgrade { get => _onActivateCatapultSecondUpgrade; set => _onActivateCatapultSecondUpgrade = value; }
    public static Action<int, GameObject> OnActivateCatapultThirdUpgrade { get => _onActivateCatapultThirdUpgrade; set => _onActivateCatapultThirdUpgrade = value; }
    public static Action<float, GameObject> OnActivateSniperFirstUpgrade { get => _onActivateSniperFirstUpgrade; set => _onActivateSniperFirstUpgrade = value; }
    public static Action<int, GameObject> OnActivateSniperSecondUpgrade { get => _onActivateSniperSecondUpgrade; set => _onActivateSniperSecondUpgrade = value; }
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


    //Action<int, float> doSmth
    //doSmth?.invoke(int, float)

    private void SetUpgradeSprite()
    {
        switch(_upgradeSO.UpgradeIndex)
        {
            case 1: _view.FirstUpgradeSprite.SetActive(true); _view.FirstUpgradeSprite.GetComponent<SpriteRenderer>().sprite = _upgradeSO.TowerSprite; break;
            case 2: break;
            case 3: break;
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
        //Дабл - шот (стреляет с микропромежутком)
        //Нужен таймер, по истечении которого будет дублироваться скрипт выстрела. Сместить для него буллетСпавнер
    }

    private void ActivateCannonSecondUpgrade()
    {
        int addDamage = _intParametrUpgrade;

        _onActivateCannonSecondUpgrade?.Invoke(addDamage, _towerObject);
    }

    private void ActivateCannonThirdUpgrade()
    {
        //Отскок снаряда
        //При попадании в цель рандомно выбирать цель в радиусе??? и менять траекторию полета
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

        //Увеличение площади
        //Просто изменить размер триггера
    }

    private void ActivateCatapultSecondUpgrade()
    {
        //Увеличение урона всем, пока в луже
        //Добавить проверку на то, находится ли цель в радиусе и добавить переменную к подсчету урона
    }

    private void ActivateCatapultThirdUpgrade()
    {
        //Увеличение частоты урона
        //Просто добавить переменную к таймеру урона
    }

    private void ActivateSniperFirstUpgrade()
    {
        //Ускорение перезарядки
        //Просто добавить переменную к таймеру перезарядки
    }

    private void ActivateSniperSecondUpgrade()
    {
        //Электризация(пост.урон)
        //Сделать что-то похожее на аоешный демедж
    }

    private void ActivateSniperThirdUpgrade()
    {
        //Увеличение дальности полета пули
        //Просто добавить переменную в проверку дальности пули
    }
}
