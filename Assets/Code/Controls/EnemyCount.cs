using System;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyScoreText;

    public int Score = 10;

    private string _enemyScore;
    private bool _isDefeat = false;
    private int _maxScore = 10;

    public bool Defeat { get => _isDefeat; set => _isDefeat = value; }

    private void Start()
    {
        _enemyScore = $"Мокриц живо: {Score} /";
        _enemyScoreText.text = $"{_enemyScore} {_maxScore}";
    }

    private void OnEnable()
    {
        EnemyMovement.OnEnemyEnter += EnemyEnterExit;
    }

    private void OnDisable()
    {
        EnemyMovement.OnEnemyEnter -= EnemyEnterExit;
    }

    private void EnemyEnterExit()
    {
        if (Score == 1)
        {
            Score--;
            _enemyScore = $"Мокриц живо: {Score} /";
            _enemyScoreText.text = $"{_enemyScore} {_maxScore}";
            _isDefeat = true;
        }
        else if (Score > 1)
        {
            Score--;
            _enemyScore = $"Мокриц живо: {Score} /";
            _enemyScoreText.text = $"{_enemyScore} {_maxScore}";
        }
    }
}
