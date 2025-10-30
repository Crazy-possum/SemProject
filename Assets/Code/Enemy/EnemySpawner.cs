using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Префаб противника")]
    [SerializeField] private GameObject _enemyPrefab;
    [Tooltip("Пустой объект, куда спавнятся противники")]
    [SerializeField] private GameObject _enemiesGroup;
    [Tooltip("Список всех противников на сцене")]
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [Tooltip("Лист с точками маршрута противников")]
    [SerializeField] private Transform[] _enemyWayPointsList;

    [Tooltip("Длительность волны в сек")]
    [SerializeField] private int _waveTimerValue;
    [Tooltip("Время на спавн одного моба в сек")]
    [SerializeField] private int _spawnTimerValue;

    private Timer _waveTimer;
    private Timer _spawnTimer;
    private int _currentEnemyListLength;

    public int CurrentEnemyListLength { get => _currentEnemyListLength; set => _currentEnemyListLength = value; }

    private void Start()
    {
        _waveTimer = new Timer(_waveTimerValue);
        _spawnTimer = new Timer(_spawnTimerValue);

        WaveBegins();
    }

    private void FixedUpdate()
    {
        _waveTimer.Wait();
        _spawnTimer.Wait();

        _currentEnemyListLength = _enemyList.Count;

        if (!_waveTimer.ReachingTimerMaxValue)
        {
            if (_spawnTimer.ReachingTimerMaxValue)
            {
                SpawnEnemy();

                _spawnTimer.StopCountdown();
                _spawnTimer.StartCountdown();
            }
        }
        else
        {
            _waveTimer.StopCountdown();
            _spawnTimer.StopCountdown();
        }

        SearchMissingObject();
    }

    private void WaveBegins()
    {
        _waveTimer.StartCountdown();
        _spawnTimer.StartCountdown();
    }

    private void SpawnEnemy()
    {
        Vector3 position = _enemyWayPointsList[0].position;
        GameObject sceneGObject = GameObject.Instantiate(_enemyPrefab, position, Quaternion.identity, _enemiesGroup.transform);

        _enemyList.Add(sceneGObject.gameObject);

        sceneGObject.GetComponent<EnemyMovement>().EnemyWayPintsList = _enemyWayPointsList;
    }

    private void SearchMissingObject()
    {
        _enemyList.RemoveAll(t => t.gameObject == null);
    }
}
