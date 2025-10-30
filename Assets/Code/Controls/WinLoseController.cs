using UnityEngine;

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

    private bool _isAllEnemiesDefeated;
    private bool _isDeafeated;
    private bool _isWin;
    private int _currentEnemyMiss;
    private int _currentEnemyCount;

    private void Start()
    {
        _isAllEnemiesDefeated = _currentEnemyCount + _currentEnemyMiss 
            >= _enemySpawner.CurrentEnemyListLength;
    }

    private void FixedUpdate()
    {
        _isDeafeated = _enemyCount.Defeat;
        _currentEnemyMiss = _enemyCount.Score;

        CheckGameComplite();
    }

    private void OnEnable()
    {
        EnemyParametrs.OnEnemyDied += CheckKillCount;
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= CheckKillCount;
    }

    private void GameStop()
    {
        Time.timeScale = 0;
    }

    private void DefeatPanel()
    {
        _defeatPanel.SetActive(true);
    }

    private void WinPanel()
    {
        _winPanel.SetActive(true);
    }

    private void CheckKillCount()
    {
        _currentEnemyCount += 1;

        if (_isAllEnemiesDefeated && _currentEnemyCount > 0)
        {
            _isWin = true;
        }
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
            WinPanel();
            GameStop();
        }
    }
}

