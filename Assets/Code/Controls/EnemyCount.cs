using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyScoreText;

    public bool Defeat = false;
    public int Score = 0;

    private int _maxScore = 5;

    private void Start()
    {
        _enemyScoreText.text = "Врагов прошло:" + 0 + "/" + _maxScore;
    }

    private void OnEnable()
    {
        EnemyMovement.EnemyEnter += EnemyEnterExit;
    }

    private void OnDisable()
    {
        EnemyMovement.EnemyEnter -= EnemyEnterExit;
    }

    private void EnemyEnterExit()
    {
        if (Score == _maxScore - 1)
        {
            Score++;
            _enemyScoreText.text = "Врагов прошло:" + Score + "/" + _maxScore;
            Defeat = true;
        }
        else if (Score < _maxScore - 1)
        {
            Score++;
            _enemyScoreText.text = "Врагов прошло:" + Score + "/" + _maxScore;
        }
    }
}
