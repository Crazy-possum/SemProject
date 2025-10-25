using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyScoreText;

    private int _maxScore = 5;
    private int _score = 0;

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
        _score++;
        _enemyScoreText.text = "Врагов прошло:" + _score + "/" + _maxScore;

    }
}
