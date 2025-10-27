using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseController : MonoBehaviour
{
    [SerializeField] private EnemyCount _enemyCount;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private GameObject _winPanel;

    private bool _isDeafeated;
    private bool _isWin;
    private int _currentEnemyMiss;
    private int _currentEnemyCount;

    private void FixedUpdate()
    {
        _isDeafeated = _enemyCount.Defeat;
        _currentEnemyMiss = _enemyCount.Score;

        CheckGameComplite();
    }

    private void OnEnable()
    {
        EnemyParametrs.EnemyDied += CheckKillCount;
    }

    private void OnDisable()
    {
        EnemyParametrs.EnemyDied -= CheckKillCount;
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

        if (_currentEnemyCount + _currentEnemyMiss >= _enemySpawner.CurrentEnemyListLength && _currentEnemyCount > 0)
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

