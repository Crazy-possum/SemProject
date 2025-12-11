using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private List<GameObject> _enemyList;
    [SerializeField] private AudioSource _enemyAS;
    [SerializeField] private AudioClip _moleClip;
    [SerializeField] private AudioClip _anteaterClip;
    [SerializeField] private AudioClip _lizardClip;
    [SerializeField] private AudioClip _ratClip;
    private Timer _moleSoundTimer;
    private Timer _anteaterSoundTimer;
    private Timer _lizardSoundTimer;
    private Timer _ratSoundTimer;
    private bool _hasMole;
    private bool _hasAnteater;
    private bool _hasLizard;
    private bool _hasRat;

    [SerializeField] private AudioSource _enemyEnterAS;
    [SerializeField] private AudioClip _enemyEnterClip;

    [SerializeField] private AudioSource _towerChangedAS;
    [SerializeField] private AudioClip _towerChangedClip;

    [SerializeField] private AudioSource _levelUpAS;
    [SerializeField] private AudioClip _levelUpClip;

    [SerializeField] private AudioSource _resultAS;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _defeatClip;

    private void Start()
    {
        _moleSoundTimer = new Timer(RandomSoundSpace());
        _anteaterSoundTimer = new Timer(RandomSoundSpace());
        _lizardSoundTimer = new Timer(RandomSoundSpace());
        _ratSoundTimer = new Timer(RandomSoundSpace());
    }

    private void OnEnable()
    {
        EnemyMovement.OnEnemyEnter += EnemyEnterSound;
        DecisionButton.OnTowerChanged += TowerChangedSound;
        ExperienceController.OnLevelUp += LevelUpSound;
        WinLoseController.OnDefeat += DefeatSound;
        WinLoseController.OnWin += WinSound;
    }

    private void FixedUpdate()
    {
        _enemyList = _enemySpawner.EnemyList;
        CheckEnemyAvailability();

        ReloadMoleTimer();
        ReloadAnteaterTimer();
        ReloadLizardTimer();
        ReloadRatTimer();
    }

    private void CheckEnemyAvailability()
    {
        _hasMole = _enemyList.Any(enemy =>
        {
            var enemyParams = enemy.GetComponent<EnemyParametrs>();
            return enemyParams != null && enemyParams.EnemySO.EnemyEnum == EnemyEnum.Mole;
        });

        _hasAnteater = _enemyList.Any(enemy =>
        {
            var enemyParams = enemy.GetComponent<EnemyParametrs>();
            return enemyParams != null && enemyParams.EnemySO.EnemyEnum == EnemyEnum.Anteater;
        });

        _hasLizard = _enemyList.Any(enemy =>
        {
            var enemyParams = enemy.GetComponent<EnemyParametrs>();
            return enemyParams != null && enemyParams.EnemySO.EnemyEnum == EnemyEnum.Lizard;
        });

        _hasRat = _enemyList.Any(enemy =>
        {
            var enemyParams = enemy.GetComponent<EnemyParametrs>();
            return enemyParams != null && enemyParams.EnemySO.EnemyEnum == EnemyEnum.Rat;
        });
    }

    private float RandomSoundSpace()
    {
        float randomFloat = Random.Range(3f, 15f);
        return randomFloat;
    }

    private void ReloadMoleTimer()
    {
        _moleSoundTimer.Wait();

        if (!_moleSoundTimer.StartTimer)
        {
            _moleSoundTimer.StartCountdown();
        }

        if (_moleSoundTimer.ReachingTimerMaxValue == true)
        {
            _moleSoundTimer.StopCountdown();

            if (_hasMole && !_enemyAS.isPlaying)
            {
                _enemyAS.PlayOneShot(_moleClip);
            }

            _moleSoundTimer.ResetTimerMaxTime(RandomSoundSpace());
        }
    }

    private void ReloadAnteaterTimer()
    {
        _anteaterSoundTimer.Wait();

        if (!_anteaterSoundTimer.StartTimer)
        {
            _anteaterSoundTimer.StartCountdown();
        }

        if (_anteaterSoundTimer.ReachingTimerMaxValue == true)
        {
            _anteaterSoundTimer.StopCountdown();

            if (_hasAnteater && !_enemyAS.isPlaying)
            {
                _enemyAS.PlayOneShot(_anteaterClip);
            }

            _anteaterSoundTimer.ResetTimerMaxTime(RandomSoundSpace());
        }
    }

    private void ReloadLizardTimer()
    {
        _lizardSoundTimer.Wait();

        if (!_lizardSoundTimer.StartTimer)
        {
            _lizardSoundTimer.StartCountdown();
        }

        if (_lizardSoundTimer.ReachingTimerMaxValue == true)
        {
            _lizardSoundTimer.StopCountdown();

            if (_hasLizard && !_enemyAS.isPlaying)
            {
                _enemyAS.PlayOneShot(_lizardClip);
            }

            _lizardSoundTimer.ResetTimerMaxTime(RandomSoundSpace());
        }
    }

    private void ReloadRatTimer()
    {
        _ratSoundTimer.Wait();

        if (!_ratSoundTimer.StartTimer)
        {
            _ratSoundTimer.StartCountdown();
        }

        if (_ratSoundTimer.ReachingTimerMaxValue == true)
        {
            _ratSoundTimer.StopCountdown();

            if (_hasRat && !_enemyAS.isPlaying)
            {
                _enemyAS.PlayOneShot(_ratClip);
            }

            _ratSoundTimer.ResetTimerMaxTime(RandomSoundSpace());
        }
    }

    private void EnemyEnterSound()
    {
        if (!_enemyEnterAS.isPlaying)
        {
            _enemyEnterAS.PlayOneShot(_enemyEnterClip);
        }
    }

    private void TowerChangedSound()
    {
        if (!_towerChangedAS.isPlaying)
        {
            _towerChangedAS.PlayOneShot(_towerChangedClip);
        }
    }

    private void LevelUpSound()
    {
        _levelUpAS.PlayOneShot(_levelUpClip);
    }

    private void DefeatSound()
    {
        _resultAS.PlayOneShot(_defeatClip);
    }
    private void WinSound()
    {
        _resultAS.PlayOneShot(_winClip);
    }
}
