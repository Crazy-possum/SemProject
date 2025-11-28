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
    private float _passExpTimerValue;
    private float _passExpIncome;
    private float _doubleKillTimerValue;

    private float _currentExp;

    public static Action OnLevelUp { get => _onLevelUp; set => _onLevelUp = value; }
    public float CurrentExp { get => _currentExp; set => _currentExp = value; }

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

        if (_isPassIncomeOn)
        {
            IncomeTimerReload(_passExpTimerValue, _passExpIncome);
        }

        Debug.Log(_currentExp);
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
        UpdateExpSlider();
    }

    private void UpdateExpSlider()
    {
        _experienceSlider.value = _currentExp;
    }

    private void CharLevelUp()
    {
        _currentExp = 0;
        UpdateExpSlider();

        _levelUpExpValue = _levelUpExpValue * _ratioExpUp;
        _experienceSlider.maxValue = _levelUpExpValue;

        _onLevelUp?.Invoke();
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

        _passExpTimerValue = incomeTimerValue;
        _passExpIncome = experienceIncome;

        _passiveIncomeTimer.ResetTimerMaxTime(_passExpTimerValue);
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
            UpdateExpSlider();
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
