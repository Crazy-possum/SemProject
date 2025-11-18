using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Пустой объект, куда спавнятся противники")]
    [SerializeField] private GameObject _enemiesGroup;
    [Tooltip("Список всех противников на сцене")]
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [Tooltip("Лист с точками маршрута противников")]
    [SerializeField] private Transform[] _enemyWayPointsList;

    [SerializeField] private LevelSO _levelConfig;

    private SpawnPresetSO _currentWave;
    private Timer _globalTimer;
    private Timer _spawnTimer;
    private bool _hasActiveWave;
    private bool _isAllWaveSpawned;
    private int _currentEnemyListLength;
    private int _currentWaveIndex;
    private int _currentEnemyIndex;
    private float _stopwatch = 2000;
    private float _spawnTimerValue;

    public int CurrentEnemyListLength { get => _currentEnemyListLength; set => _currentEnemyListLength = value; }
    public List<GameObject> EnemyList { get => _enemyList; set => _enemyList = value; }
    public bool IsAllWaveSpawned { get => _isAllWaveSpawned; set => _isAllWaveSpawned = value; }

    private void Start()
    {
        _globalTimer = new Timer(_stopwatch);
        _globalTimer.StartCountdown();

        _currentWaveIndex = 0;
        _currentWave = _levelConfig.WavePresetList[_currentWaveIndex];
    }

    private void FixedUpdate()
    {
        _globalTimer.Wait();
        CheckTime();

        if (_hasActiveWave)
        {
            ReloadSpawnTimer();
        }

        SearchMissingObject();
    }

    private void CheckTime()
    {
        if (_globalTimer.TimerCurrentTime >= _currentWave.waveStartTime && !_hasActiveWave && !_isAllWaveSpawned)
        {
            WaveBegins();
        }
    }

    private void ReloadSpawnTimer()
    {
        _spawnTimer.Wait();

        if (_spawnTimer.ReachingTimerMaxValue)
        {
            _spawnTimer.StopCountdown();
            SpawnEnemy();
            _spawnTimer.StartCountdown();
        }
    }

    private void WaveBegins()
    {
        _spawnTimerValue = _currentWave.spawnTimerValue;
        _spawnTimer = new Timer(_spawnTimerValue);
        _spawnTimer.StartCountdown();

        _hasActiveWave = true;
    }

    private void SpawnEnemy()
    {
        if (_currentEnemyIndex <= _currentWave.enemySequenceList.Count - 1)
        {
            GameObject enemyPrefab = _currentWave.enemySequenceList[_currentEnemyIndex];

            Vector3 position = _enemyWayPointsList[0].position;
            GameObject sceneGObject = GameObject.Instantiate(enemyPrefab, position, Quaternion.identity, _enemiesGroup.transform);

            _enemyList.Add(sceneGObject.gameObject);
            sceneGObject.GetComponent<EnemyMovement>().EnemyWayPintsList = _enemyWayPointsList;

            _currentEnemyIndex++;
        }
        else
        {
            WaveComplite();
        }   
    }

    private void WaveComplite()
    {
        if (_currentWaveIndex < _levelConfig.WavePresetList.Count - 1)
        {
            _currentEnemyIndex = 0;
            _currentWaveIndex++;
            _currentWave = _levelConfig.WavePresetList[_currentWaveIndex];
        }
        else
        {
            _isAllWaveSpawned = true;
        }

            _hasActiveWave = false;
    }

    private void SearchMissingObject()
    {
        _enemyList.RemoveAll(t => t.gameObject == null);
    }
}
