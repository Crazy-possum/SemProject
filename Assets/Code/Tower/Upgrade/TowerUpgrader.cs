using System;
using System.Threading.Tasks;
using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    private GameObject _towerObject;
    private TowerUpgradeSO _upgradeSO;
    private TowerView _view;
    private ScriptableListScript _towerUpgradeListSO;

    private static Action<float> _on;
    private static Action<int> _onActivateCannonSecondUpgrade;

    private int _parametrUpgrade;

    public static Action<float> On { get => _on; set => _on = value; }
    public static Action<int> OnActivateCannonSecondUpgrade { get => _onActivateCannonSecondUpgrade; set => _onActivateCannonSecondUpgrade = value; }

    private void Awake()
    {
        _towerUpgradeListSO = Resources.Load<ScriptableListScript>("Tower/Upgrades/TowerUpgrades_SO");
    }

    public void SetTowerUpgrade(GameObject tower, TowerUpgradeSO upgradeSO)
    {
        _upgradeSO = upgradeSO;
        _towerObject = tower;
        _parametrUpgrade = upgradeSO.UpgradeIntValue;
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
        //_onEnemyDied?.Invoke();
    }

    private void ActivateCannonSecondUpgrade()
    {
        int addDamage = _parametrUpgrade;

        OnActivateCannonSecondUpgrade.Invoke(addDamage);
        Debug.Log(1);
        
        
        //Увеличение урона 
        //Просто добавить перменную в метод подсчета урона
    }

    private void ActivateCannonThirdUpgrade()
    {
        //Отскок снаряда
        //При попадании в цель рандомно выбирать цель в радиусе??? и менять траекторию полета
    }

    private void ActivateShotgunFirstUpgrade()
    {
        //Увеличение количества дробинок
        //Просто добавить переменную в метод пересчета снарядов
    }

    private void ActivateShotgunSecondUpgrade()
    {
        //Увеличение радиуса
        //Просто изменить размер триггера
    }

    private void ActivateShotgunThirdUpgrade()
    {
        //Ускорение перезарядки
        //Просто добавить переменную к таймеру перезарядки
    }

    private void ActivateCatapultFirstUpgrade()
    {
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
