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
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= ExperienceIncome;
    }

    private void ExperienceIncome()
    {
        _currentExp += _currentExpIncome;
        _experienceSlider.value = _currentExp;
    }

    private void CharLevelUp()
    {
        _currentExp = 0;
        _levelUpExpValue = _levelUpExpValue * _ratioExpUp;
        _experienceSlider.maxValue = _levelUpExpValue;

        _onLevelUp?.Invoke();
    }
}
