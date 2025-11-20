using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUpgrader : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _charUpgradePanel;

    private List<DecisionButton> _charButtonList = new List<DecisionButton>();
    private List<CharUpgradeRareSO> _charUpgradeRareSOList;
    private List<CharUpgradeSO> _charRegularUpgradeSOList;
    private List<CharUpgradeSO> _charRareUpgradeSOList;
    private List<CharUpgradeSO> _charLegendaryUpgradeSOList;
    private List<CharUpgradeSO> _charUniqueUpgradeSOList;

    private static Action<float, float> _onExperienceIncome;
    private static Action<float> _onIncreaseTowerDamage;
    private static Action<float> _onIncreaseTowerRadius;
    private static Action<float, int> _onMoneyIncome;
    private static Action<float, int> _onSlowDownMobs;
    private static Action<int> _onSlowMobsMove;
    private static Action<float> _onSpeedUpCharReload;
    private static Action<float> _onSpeedUpTowerReload;
    private static Action _onDoubleKill;
    private static Action _onDoublePaint;
    private static Action _onSlingshot;
    private static Action _onTeleport;

    private CharacterUpgrader _charUpgrader;
    private CharUpgradeSO _charUpgradeSO;
    private int _intParametrUpgrade;
    private float _floatParametrUpgrade;
    private float _addFloatParametrUpgrade;
    private bool _isCharButtonHere;

    public static Action<float, float> OnExperienceIncome { get => _onExperienceIncome; set => _onExperienceIncome = value; }
    public static Action<float> OnIncreaseTowerDamage { get => _onIncreaseTowerDamage; set => _onIncreaseTowerDamage = value; }
    public static Action<float> OnIncreaseTowerRadius { get => _onIncreaseTowerRadius; set => _onIncreaseTowerRadius = value; }
    public static Action<float, int> OnMoneyIncome { get => _onMoneyIncome; set => _onMoneyIncome = value; }
    public static Action<float, int> OnSlowDownMobs { get => _onSlowDownMobs; set => _onSlowDownMobs = value; }
    public static Action<int> OnSlowMobsMove { get => _onSlowMobsMove; set => _onSlowMobsMove = value; }
    public static Action<float> OnSpeedUpCharReload { get => _onSpeedUpCharReload; set => _onSpeedUpCharReload = value; }
    public static Action<float> OnSpeedUpTowerReload { get => _onSpeedUpTowerReload; set => _onSpeedUpTowerReload = value; }
    public static Action OnDoubleKill { get => _onDoubleKill; set => _onDoubleKill = value; }
    public static Action OnDoublePaint { get => _onDoublePaint; set => _onDoublePaint = value; }
    public static Action OnSlingshot { get => _onSlingshot; set => _onSlingshot = value; }
    public static Action OnTeleport { get => _onTeleport; set => _onTeleport = value; }

    private void Awake()
    {
        _charUpgradeRareSOList = Resources.Load<ScriptableListScript>("Character/CharUpgradesList").CharUpgradeRareSOList;
        _charUpgrader = gameObject.GetComponent<CharacterUpgrader>();

        foreach (CharUpgradeRareSO list in _charUpgradeRareSOList)
        {
            if (list.CharUpgradeRare == CharUpgradeRareEnum.Regular)
            {
                _charRegularUpgradeSOList = list.CharUpgradeSOList;
            }
            else if (list.CharUpgradeRare == CharUpgradeRareEnum.Rare)
            {
                _charRareUpgradeSOList = list.CharUpgradeSOList;
            }
            else if (list.CharUpgradeRare == CharUpgradeRareEnum.Legendary)
            {
                _charLegendaryUpgradeSOList = list.CharUpgradeSOList;
            }
            else if (list.CharUpgradeRare == CharUpgradeRareEnum.Unique)
            {
                _charUniqueUpgradeSOList = list.CharUpgradeSOList;
            }
        }
    }

    private void OnEnable()
    {
        ExperienceController.OnLevelUp += InitializeCharacterButton;
    }

    private void OnDisable()
    {
        ExperienceController.OnLevelUp -= InitializeCharacterButton;
    }

    private void InitializeCharacterButton()
    {
        if (!_isCharButtonHere)
        {
            int buttonAmount = 3;

            for (int i = 0; i < buttonAmount; i++)
            {
                GameObject uiGObject = GameObject.Instantiate(_buttonPrefab, _charUpgradePanel.GetComponentInChildren<LayoutGroup>().gameObject.transform);
                DecisionButton buttonScript = uiGObject.GetComponentInChildren<DecisionButton>();

                _charButtonList.Add(buttonScript);
            }
            _isCharButtonHere = true;
        }

        foreach (DecisionButton button in _charButtonList)
        {
            SpinUpgradesRare(button);
        }
    }

    private void SpinUpgradesRare(DecisionButton button)
    {
        System.Random rnd = new System.Random();
        int randIndex = rnd.Next(_charUpgradeRareSOList.Count);

        if (_charUpgradeRareSOList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Regular)
        {
            SpineUpgrade(_charRegularUpgradeSOList, button);
        }
        else if (_charUpgradeRareSOList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Rare)
        {
            SpineUpgrade(_charRareUpgradeSOList, button);
        }
        else if (_charUpgradeRareSOList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Legendary)
        {
            SpineUpgrade(_charLegendaryUpgradeSOList, button);
        }
        else if (_charUpgradeRareSOList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Unique)
        {
            SpineUpgrade(_charUniqueUpgradeSOList, button);
        }
    }

    private void SpineUpgrade(List<CharUpgradeSO> upgradeList, DecisionButton button)
    {
        System.Random rnd = new System.Random();
        int randIndex = rnd.Next(upgradeList.Count);

        CharUpgradeSO charUpgradeSO = upgradeList[randIndex];
        button.CharUpgradePanel = _charUpgradePanel;
        button.CharacterUpgrader1 = _charUpgrader;
        button.CustomizationCharacterButton(charUpgradeSO);

        if (upgradeList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Regular)
        {
            _charRegularUpgradeSOList.Remove(charUpgradeSO);
        }
        else if (upgradeList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Rare)
        {
            _charRareUpgradeSOList.Remove(charUpgradeSO);
        }
        else if (upgradeList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Legendary)
        {
            _charLegendaryUpgradeSOList.Remove(charUpgradeSO);
        }
        else if (upgradeList[randIndex].CharUpgradeRare == CharUpgradeRareEnum.Unique)
        {
            _charUniqueUpgradeSOList.Remove(charUpgradeSO);
        }
    }

    public void ApplyUpgrade(CharUpgradeSO charUpgradeSO)
    {
        _charUpgradeSO = charUpgradeSO;
        _intParametrUpgrade = _charUpgradeSO.UpgradeIntValue;
        _floatParametrUpgrade = _charUpgradeSO.UpgradeFloatValue;
        _addFloatParametrUpgrade = _charUpgradeSO.AddUpgradeFloatValue;

        ChooseUpgradeImpact();
    }

    private void ChooseUpgradeImpact()
    {
        if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.ExperienceIncome)
        {
            ActivateExperienceIncome();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.IncreaseTowerDamage)
        {
            ActivateIncreaseTowerDamage();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.IncreaseTowerRadius)
        {
            ActivateIncreaseTowerRadius();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.MoneyIncome)
        {
            ActivateMoneyIncome();
        }
        else if(_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.SlowDownMobs)
        {
            ActivateSlowDownMobs();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.SlowMobsMove)
        {
            ActivateSlowMobsMove();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.SpeedUpCharReload)
        {
            ActivateSpeedUpCharReload();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.SpeedUpTowerReload)
        {
            ActivateSpeedUpTowerReload();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.DoubleKill)
        {
            ActivateDoubleKill();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.DoublePaint)
        {
            ActivateDoublePaint();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.Slingshot)
        {
            ActivateSlingshot();
        }
        else if (_charUpgradeSO.UpgradeEnum == CharacterUpgradeEnum.Teleport)
        {
            ActivateTeleport();
        }
    }

    private void ActivateExperienceIncome()
    {
        float incomeTimerValue = _floatParametrUpgrade;
        float experienceIncome = _addFloatParametrUpgrade;

        _onExperienceIncome?.Invoke(incomeTimerValue, experienceIncome); //+++
    }

    private void ActivateIncreaseTowerDamage()
    {
        float towerDamage = _floatParametrUpgrade;

        _onIncreaseTowerDamage?.Invoke(towerDamage); //+++
    }

    private void ActivateIncreaseTowerRadius()
    {
        float towerRange = _floatParametrUpgrade;

        _onIncreaseTowerRadius?.Invoke(towerRange); //+++
    }

    private void ActivateMoneyIncome()
    {
        float incomeTimerValue = _floatParametrUpgrade;
        int moneyIncome = _intParametrUpgrade;

        _onMoneyIncome?.Invoke(incomeTimerValue, moneyIncome); //+++
    }

    private void ActivateSlowDownMobs()
    {
        float debuffTimerValue = _floatParametrUpgrade;
        int slowingDown = _intParametrUpgrade;

        _onSlowDownMobs?.Invoke(debuffTimerValue, slowingDown); //+++
    }

    private void ActivateSlowMobsMove()
    {
        int slowingDown = _intParametrUpgrade;

        _onSlowMobsMove?.Invoke(slowingDown); //+++
    }

    private void ActivateSpeedUpCharReload()
    {
        float cutCharReload = _floatParametrUpgrade;

        _onSpeedUpCharReload?.Invoke(cutCharReload);
    }

    private void ActivateSpeedUpTowerReload()
    {
        float cutTowerReload = _floatParametrUpgrade;

        _onSpeedUpTowerReload?.Invoke(cutTowerReload);
    }

    private void ActivateDoubleKill()
    {
        _onDoubleKill?.Invoke();
    }

    private void ActivateDoublePaint()
    {
        _onDoublePaint?.Invoke();
    }

    private void ActivateSlingshot()
    {
        _onSlingshot?.Invoke();
    }

    private void ActivateTeleport()
    {
        _onTeleport?.Invoke();
    }
}
