using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private EconomyController _economyController;
    [Tooltip("Панель с выбором башни на постройку")]
    [SerializeField] private GameObject _towerBuildPanel;
    [SerializeField] private GameObject _towerUpgradePanel;
    [SerializeField] private GameObject _towerButton;

    private List<DecisionButton> _upgradeButtonList = new List<DecisionButton>();
    private ScriptableListScript _towerObjectListSO;
    private ScriptableListScript _towerUpgradeListSO;
    private bool _isTownBuildButtenHere;
    private bool _isTownUpgradeButtenHere;

    private void Start()
    {
        _towerObjectListSO = Resources.Load<ScriptableListScript>("Tower/TowerObjects_SO");
        _towerUpgradeListSO = Resources.Load<ScriptableListScript>("Tower/Upgrades/TowerUpgrades_SO");
    }

    public void ClickBehavior(GameObject objectUnderMouse)
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (objectUnderMouse.TryGetComponent(out BuildPointTag towerBehavior))
            {
                InitializeBuildButton();
                _towerBuildPanel.SetActive(true);
            }
            else if(objectUnderMouse.TryGetComponent(out TowerAttak towerAttak))
            {
                InitializeUpgradeButton(objectUnderMouse);
                _towerUpgradePanel.SetActive(true);
            }

            _towerBuilder.BuildPointObject = objectUnderMouse;
        }
    }

    private void InitializeBuildButton()
    {
        if (!_isTownBuildButtenHere)
        {
            int buttonAmount = 4;

            for (int i = 0; i < buttonAmount; i++)
            {
                GameObject uiGObject = GameObject.Instantiate(_towerButton, _towerBuildPanel.GetComponentInChildren<LayoutGroup>().gameObject.transform);
                DecisionButton buttonScript = uiGObject.GetComponentInChildren<DecisionButton>();

                switch (i)
                {
                    case 0: buttonScript.LocalTowerEnum = TowerEnum.Cannon; break;
                    case 1: buttonScript.LocalTowerEnum = TowerEnum.Shotgun; break;
                    case 2: buttonScript.LocalTowerEnum = TowerEnum.Catapult; break;
                    case 3: buttonScript.LocalTowerEnum = TowerEnum.Sniper; break;
                }

                TowerScriptable towerSO = _towerObjectListSO.TowerSOList.Find(item => item.TowerEnum == buttonScript.LocalTowerEnum);
                buttonScript.TowerBuilder = _towerBuilder;
                buttonScript.TowerUpgrader = _towerBuilder.GetComponentInParent<TowerUpgrader>();
                buttonScript.EconomyController = _economyController;
                buttonScript.TowerBuildPanel = _towerBuildPanel;
                buttonScript.CustomizationBuildButton(towerSO);
            }
            _isTownBuildButtenHere = true;
        }
    }

    private void InitializeUpgradeButton(GameObject currentObject)
    {
        if (!_isTownUpgradeButtenHere)
        {
            int buttonAmount = 3;

            for (int i = 0; i < buttonAmount; i++)
            {
                GameObject uiGObject = GameObject.Instantiate(_towerButton, _towerUpgradePanel.GetComponentInChildren<LayoutGroup>().gameObject.transform);
                DecisionButton buttonScript = uiGObject.GetComponentInChildren<DecisionButton>();

                _upgradeButtonList.Add(buttonScript);
            }

            _isTownUpgradeButtenHere = true;
        }

        CheckTowerType(_upgradeButtonList, currentObject); 
    }

    private void CheckTowerType(List<DecisionButton> buttonScriptList, GameObject currentObject)
    {
        TowerEnum towerEnum = currentObject.GetComponent<TowerAttak>().TowerSO.TowerEnum;

        for (int i = 0; i < buttonScriptList.Count; i++)
        {
            DecisionButton currentButton = buttonScriptList[i];

            if (towerEnum == TowerEnum.Cannon)
            {
                switch (i)
                {
                    case 0: currentButton.LocalTowerEnum = TowerEnum.Cannon_firstUpgrade; break;
                    case 1: currentButton.LocalTowerEnum = TowerEnum.Cannon_secondUpgrade; break;
                    case 2: currentButton.LocalTowerEnum = TowerEnum.Cannon_thirdUpgrade; break;
                }
            }
            else if (towerEnum == TowerEnum.Shotgun)
            {
                switch (i)
                {
                    case 0: currentButton.LocalTowerEnum = TowerEnum.Shotgun_firstUpgrade; break;
                    case 1: currentButton.LocalTowerEnum = TowerEnum.Shotgun_secondUpgrade; break;
                    case 2: currentButton.LocalTowerEnum = TowerEnum.Shotgun_thirdUpgrade; break;
                }
            }
            else if (towerEnum == TowerEnum.Catapult)
            {
                switch (i)
                {
                    case 0: currentButton.LocalTowerEnum = TowerEnum.Catapult_firstUpgrade; break;
                    case 1: currentButton.LocalTowerEnum = TowerEnum.Catapult_secondUpgrade; break;
                    case 2: currentButton.LocalTowerEnum = TowerEnum.Catapult_thirdUpgrade; break;
                }
            }
            else if (towerEnum == TowerEnum.Sniper)
            {
                switch (i)
                {
                    case 0: currentButton.LocalTowerEnum = TowerEnum.Sniper_firstUpgrade; break;
                    case 1: currentButton.LocalTowerEnum = TowerEnum.Sniper_secondUpgrade; break;
                    case 2: currentButton.LocalTowerEnum = TowerEnum.Sniper_thirdUpgrade; break;
                }
            }

            TowerUpgradeSO upgradeSO = _towerUpgradeListSO.TowerUpgradeSOList.Find(item => item.TowerEnum == currentButton.LocalTowerEnum);
            currentButton.TowerBuilder = _towerBuilder;
            currentButton.TowerUpgrader = _towerBuilder.GetComponentInParent<TowerUpgrader>();
            currentButton.TowerUpgradePanel = _towerUpgradePanel;
            currentButton.EconomyController = _economyController;
            currentButton.Tower = currentObject;
            currentButton.CustomizationUpgradeButton(upgradeSO);
        }
    }
}
