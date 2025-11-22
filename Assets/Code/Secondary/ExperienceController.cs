using System;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceController : MonoBehaviour
{
    [Tooltip("Слайдер опыта")]
    [SerializeField] private Slider _experienceSlider;
    [Tooltip("Количество опыта для набора первого уровня")]
    [SerializeField] private float _levelUpExpValue;
    [Tooltip("Коэффициент увеличения требований к опыту от уровня")]
    [SerializeField] private float _ratioExpUp;
    [Tooltip("Значение прироста опыта от убийства")]
    [SerializeField] private float _currentExpIncome;

    private static Action _onLevelUp;

    private Timer _passiveIncomeTimer;
    private Timer _doubleKillTimer;
    private bool _isPassIncomeOn;
    private bool _isDoubleKillOn;
    private bool _isWasMurder;
    private float _doubleKillTimerValue;

    private float _currentExp;

    public static Action OnLevelUp { get => _onLevelUp; set => _onLevelUp = value; }

    private void Start()
    {
        _experienceSlider.value = 0;
        _experienceSlider.maxValue = _levelUpExpValue;
    }

    private void FixedUpdate()
    {
        if (_currentExp >= _levelUpExpValue)
        {
            CharLevelUp();
        }
    }

    private void OnEnable()
    {
        EnemyParametrs.OnEnemyDied += ExperienceIncome;
        CharacterUpgrader.OnExperienceIncome += PassiveExperienceIncome;
        CharacterUpgrader.OnDoubleKill += ActivateDoubleKill;

        if (_isDoubleKillOn)
        {
            EnemyParametrs.OnEnemyDied += DetectDoubleKill;
        }
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= ExperienceIncome;
        EnemyParametrs.OnEnemyDied -= DetectDoubleKill;
        CharacterUpgrader.OnExperienceIncome -= PassiveExperienceIncome;
        CharacterUpgrader.OnDoubleKill -= ActivateDoubleKill;
    }

    private void ExperienceIncome()
    {
        _currentExp += _currentExpIncome;
        _experienceSlider.value = _currentExp;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    #region addLiseners
    private void PassiveExperienceIncome(float incomeTimerValue, float experienceIncome)
    {
        if (!_isPassIncomeOn)
        {
            _passiveIncomeTimer = new Timer(incomeTimerValue);
            _isPassIncomeOn = true;
        }

        IncomeTimerReload(incomeTimerValue, experienceIncome);
    }

    private void IncomeTimerReload(float incomeTimerValue, float experienceIncome)
    {
        _passiveIncomeTimer.Wait();

        if (!_passiveIncomeTimer.StartTimer)
        {
            _passiveIncomeTimer.StartCountdown();
        }

        if (_passiveIncomeTimer.ReachingTimerMaxValue == true)
        {
            _passiveIncomeTimer.StopCountdown();
            _currentExp += experienceIncome;
            PassiveExperienceIncome(incomeTimerValue, experienceIncome);
        }
        else 
        {
            IncomeTimerReload(incomeTimerValue, experienceIncome);
        }
    }

    private void CharLevelUp()
    {
        _currentExp = 0;
        _levelUpExpValue = _levelUpExpValue * _ratioExpUp;
        _experienceSlider.maxValue = _levelUpExpValue;

        _onLevelUp?.Invoke();
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
            ExperienceIncome();
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
    #endregion
}
