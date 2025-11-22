using TMPro;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    [Tooltip("Текст текущей валюты")]
    [SerializeField] private TMP_Text _currencyText;

    public int GeneralCurrency;
    public int CurrentCost;
    public int CurrentIncome;

    private Timer _passiveIncomeTimer;
    private Timer _doubleKillTimer;
    private bool _isPassIncomeOn;
    private bool _isDoubleKillOn;
    private bool _isWasMurder;
    private float _doubleKillTimerValue;

    private void Start()
    {
        _currencyText.text = GeneralCurrency.ToString();
    }

    private void OnEnable()
    {
        EnemyParametrs.OnEnemyDied += CurrencySum;
        CharacterUpgrader.OnMoneyIncome += PassiveMoneyIncome;
        CharacterUpgrader.OnDoubleKill += ActivateDoubleKill;

        if (_isDoubleKillOn)
        {
            EnemyParametrs.OnEnemyDied += DetectDoubleKill;
        }
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= CurrencySum;
        EnemyParametrs.OnEnemyDied -= DetectDoubleKill;
        CharacterUpgrader.OnMoneyIncome -= PassiveMoneyIncome;
        CharacterUpgrader.OnDoubleKill -= ActivateDoubleKill;
    }

    private void CurrencySum()
    {
        GeneralCurrency += CurrentIncome;
        _currencyText.text = GeneralCurrency.ToString();
    }

    public void SpendCurrency()
    {
        GeneralCurrency -= CurrentCost;
        _currencyText.text = GeneralCurrency.ToString();
    }

    private void PassiveMoneyIncome(float incomeTimerValue, int moneyIncome)
    {
        if (!_isPassIncomeOn)
        {
            _passiveIncomeTimer = new Timer(incomeTimerValue);
            _isPassIncomeOn = true;
        }

        IncomeTimerReload(incomeTimerValue, moneyIncome);
    }

    private void IncomeTimerReload(float incomeTimerValue, int moneyIncome)
    {
        _passiveIncomeTimer.Wait();

        if (!_passiveIncomeTimer.StartTimer)
        {
            _passiveIncomeTimer.StartCountdown();
        }

        if (_passiveIncomeTimer.ReachingTimerMaxValue == true)
        {
            _passiveIncomeTimer.StopCountdown();
            GeneralCurrency += moneyIncome;
            PassiveMoneyIncome(incomeTimerValue, moneyIncome);
        }
    }

    private void ActivateDoubleKill(float doubleKillTimerValue)
    {
        _isDoubleKillOn = true;
        _doubleKillTimerValue = doubleKillTimerValue;
    }

    private void DetectDoubleKill()
    {
        if (_isWasMurder)
        {
            _isWasMurder = false;
            CurrencySum();
        }
        else
        {
            _isWasMurder = true;

            if (_doubleKillTimer == null)
            {
                _doubleKillTimer = new Timer(_doubleKillTimerValue);
            }
        }
    }

    private void ReloadDoubleKillTimer()
    {
        _doubleKillTimer.Wait();

        if (!_doubleKillTimer.StartTimer)
        {
            _doubleKillTimer.StartCountdown();
        }

        if (_doubleKillTimer.ReachingTimerMaxValue == true)
        {
            _doubleKillTimer.StopCountdown();
            _isWasMurder = false;
        }
        else
        {
            ReloadDoubleKillTimer();
        }
    }
}
