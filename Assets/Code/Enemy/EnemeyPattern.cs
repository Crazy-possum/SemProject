using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyPattern : MonoBehaviour
{
    [SerializeField] private EnemyParametrs _enemyParametrs;
    [SerializeField] private EnemyMovement _enemyMovement;

    [SerializeField] private List<EnemyParametrs> _healTargetsList = new List<EnemyParametrs>();

    private Timer _shieldTimer;
    private Timer _resistanceTimer;
    private EnemyEnum _enemyEnum;

    private float _finishingBoarder = 0.3f;
    private float _shieldTimerValue = 5;
    private float _resistanceTimerValue = 0.5f;
    private float _healValue = 300;
    private float _staticHealth;
    private bool _isAnteater;
    private bool _isLizard;
    private bool _isRat;
    private bool _isHeal;

    public List<EnemyParametrs> HealTargetsList { get => _healTargetsList; set => _healTargetsList = value; }

    private void Start()
    {
        _shieldTimer = new Timer(_shieldTimerValue);
        _resistanceTimer = new Timer(_resistanceTimerValue);
        _enemyEnum = _enemyParametrs.EnemySO.EnemyEnum;

        CheckEnemyEnum();
    }

    private void FixedUpdate()
    {
        if (_isAnteater)
        {
            ReloadShieldTimer();

            if (_resistanceTimer.ReachingTimerMaxValue != true)
            {
                _enemyParametrs.CurrentHealth = _staticHealth;
            }
        }

        if (_enemyParametrs.MaxHealth - _enemyParametrs.CurrentHealth <= _enemyParametrs.MaxHealth * _finishingBoarder)
        {
            if (_isLizard)
            {
                _enemyMovement.Speed *= 1.5f;
            }

            if (_isRat)
            {
                if (!_isHeal)
                {
                    HealArea();
                }
            }
        }
    }

    private void CheckEnemyEnum()
    {
        switch (_enemyEnum)
        {
            case EnemyEnum.Mole: break;
            case EnemyEnum.Anteater:ActivateAnteaterPattern(); break;
            case EnemyEnum.Lizard:ActivateLizardPattern(); break;
            case EnemyEnum.Rat: ActivateRatPattern(); break;
        }
    }

    private void ActivateAnteaterPattern()
    {
        _isAnteater = true;
    }

    private void ActivateLizardPattern()
    {
        _isLizard = true;
    }

    private void ActivateRatPattern()
    {
        _isRat = true;
    }

    private void ReloadShieldTimer()
    {
        _shieldTimer.Wait();

        if (!_shieldTimer.StartTimer)
        {
            _shieldTimer.StartCountdown();
        }

        if (_shieldTimer.ReachingTimerMaxValue == true)
        {
            _shieldTimer.StopCountdown();
            ReloadResistanceTimer();
            _staticHealth = _enemyParametrs.CurrentHealth;
        }
    }

    private void ReloadResistanceTimer()
    {
        _resistanceTimer.Wait();

        if (!_resistanceTimer.StartTimer)
        {
            _resistanceTimer.StartCountdown();
        }

        if (_resistanceTimer.ReachingTimerMaxValue == true)
        {
            _resistanceTimer.StopCountdown();
            ReloadShieldTimer();
        }
    }

    private void HealArea()
    {
        _isHeal = true;

        gameObject.GetComponent<EnemyParametrs>().CurrentHealth += _healValue;

        foreach (EnemyParametrs target in _healTargetsList)
        {
            target.CurrentHealth += _healValue;
        }
    }
}
