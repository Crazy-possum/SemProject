using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseController : MonoBehaviour
{
    [Tooltip("Скрипт")]
    [SerializeField] private EnemyCount _enemyCount;
    [Tooltip("Скрипт")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [Tooltip("Панель с проигрышем")]
    [SerializeField] private GameObject _defeatPanel;
    [Tooltip("Панель с победой")]
    [SerializeField] private GameObject _winPanel;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _winAudioClip;
    [SerializeField] private AudioClip _defeatAudioClip;

    private static Action<int> _onCompliteLevel;

    private bool _isDeafeated = false;
    private bool _isWin = false;
    private int _currentEnemyMiss;
    private int _currentEnemyCount;
    private int _currentEnemyListLength;

    public static Action<int> OnCompliteLevel { get => _onCompliteLevel; set => _onCompliteLevel = value; }

    private void FixedUpdate()
    {
        _isDeafeated = _enemyCount.Defeat;
        _currentEnemyMiss = _enemyCount.Score;
        _currentEnemyListLength = _enemySpawner.EnemyList.Count;

        if (_currentEnemyListLength == 0 && _currentEnemyCount > 0 && _enemySpawner.IsAllWaveSpawned)
        {
            _isWin = true;
        }

        CheckGameComplite();
    }

    private void OnEnable()
    {
        EnemyParametrs.OnEnemyDied += CheckKillCount;
        TutorStageActivator.OnNeedsWin += WinPanel;
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= CheckKillCount;
        TutorStageActivator.OnNeedsWin -= WinPanel;
    }

    private void GameStop()
    {
        Time.timeScale = 0;
    }

    private void DefeatPanel()
    {
        _defeatPanel.SetActive(true);
        _audioSource.PlayOneShot(_defeatAudioClip);
    }

    private void WinPanel()
    {
        _winPanel.SetActive(true);
        _audioSource.PlayOneShot(_winAudioClip);

        int index = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void CheckKillCount()
    {
        _currentEnemyCount += 1;
    }

    private void CheckGameComplite()
    {
        if (_isDeafeated)
        {
            DefeatPanel();
            GameStop();
        }
        else if (!_isDeafeated && _isWin)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.OwnPlay}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}") == false.ToString())
            {
                TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}");
                TutorController.OnTutorActive?.Invoke();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2 &&
                PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.SecondTowerBuild}" &&
                PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
                PlayerPrefs.GetString($"{TutorConstantMaganer.OWN_PLAY}") == false.ToString())
            {
                TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}");
                TutorController.OnTutorActive?.Invoke();
            }
            else
            {
                WinPanel();
                GameStop();
            }
        }
    }
}

