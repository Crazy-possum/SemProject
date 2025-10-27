using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemiesGroup;
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] private Transform[] _enemyWayPintsList;

    [SerializeField] private int _waveTimerValue;
    [SerializeField] private int _spawnTimerValue;

    public int CurrentEnemyListLength;

    private Timer _waveTimer;
    private Timer _spawnTimer;

    private EnemyMovement _enemyMovement;

    private void Start()
    {
        _waveTimer = new Timer(_waveTimerValue);
        _spawnTimer = new Timer(_spawnTimerValue);

        WaveBegins();
    }

    private void Update()
    {
        _waveTimer.Wait();
        _spawnTimer.Wait();

        CurrentEnemyListLength = _enemyList.Count;

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
        _enemyMovement = null;

        Vector3 position = _enemyWayPintsList[0].position;
        GameObject sceneGObject = GameObject.Instantiate(_enemyPrefab, position, Quaternion.identity, _enemiesGroup.transform);

        _enemyList.Add(sceneGObject.gameObject);

        _enemyMovement = sceneGObject.GetComponent<EnemyMovement>();
        _enemyMovement.EnemyWayPintsList = _enemyWayPintsList;
    }

    private void SearchMissingObject()
    {
        _enemyList.RemoveAll(t => t.gameObject == null);
    }
}
