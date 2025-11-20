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
    private bool _isPassIncomeOn;

    private float _currentExp;

    public static Action OnLevelUp { get => _onLevelUp; set => _onLevelUp = value; }

    private void Start()
    {
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
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= ExperienceIncome;
        CharacterUpgrader.OnExperienceIncome -= PassiveExperienceIncome;
    }

    private void ExperienceIncome()
    {
        _currentExp += _currentExpIncome;
        _experienceSlider.value = _currentExp;
    }

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
    }

    private void CharLevelUp()
    {
        _currentExp = 0;
        _levelUpExpValue = _levelUpExpValue * _ratioExpUp;
        _experienceSlider.maxValue = _levelUpExpValue;

        _onLevelUp?.Invoke();
    }
}
