using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemiesGroup;
    [SerializeField] private Transform[] _enemyWayPintsList;
    //[SerializeField] private Transform _enemySpawner;
    [SerializeField] private Timer _waveTimer;
    [SerializeField] private Timer _spawnTimer;

    [SerializeField] private int _waveTimerValue;
    [SerializeField] private int _spawnTimerValue;

    private EnemyMovement _enemyMovement;

    private void Start()
    {
        _waveTimer.MaxTimerValue = _waveTimerValue;
        _spawnTimer.MaxTimerValue = _spawnTimerValue;

        WaveBegins();
    }

    private void Update()
    {
        if (!_waveTimer.ReachingTimerMaxValue)
        {
            if (_spawnTimer.ReachingTimerMaxValue)
            {
                SpawnEnemy();

                _spawnTimer.TimerCurrentTime = 0;
                _spawnTimer.StopCountdown();
                _spawnTimer.StartCountdown();
            }
        }
        else
        {
            _waveTimer.StopCountdown();
            _spawnTimer.StopCountdown();
        }
    }

    private void WaveBegins()
    {
        _waveTimer.StartCountdown();
        _spawnTimer.StartCountdown();
    }

    private void SpawnEnemy()
    {
        _enemyMovement = null;

        Vector3 position = _enemyWayPintsList[0].position;
        GameObject sceneGObject = GameObject.Instantiate(_enemyPrefab, position, Quaternion.identity, _enemiesGroup.transform);

        _enemyMovement = sceneGObject.GetComponent<EnemyMovement>();
        _enemyMovement.EnemyWayPintsList = _enemyWayPintsList;
    }
}
