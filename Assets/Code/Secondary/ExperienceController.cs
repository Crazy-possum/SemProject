using UnityEngine;
using UnityEngine.UI;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private Slider _experienceSlider;
    [SerializeField] private float _levelUpExpValue;
    [SerializeField] private float _ratioExpUp;
    [SerializeField] private float _currentExp;
    [SerializeField] private float _currentExpIncome;

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
    }
}
