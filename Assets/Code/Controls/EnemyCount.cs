using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyScoreText;

    public int Score = 0;

    private const string _enemyScore = "Врагов прошло: 0 /" ;
    private bool _isDefeat = false;
    private int _maxScore = 5;

    public bool Defeat { get => _isDefeat; set => _isDefeat = value; }

    private void Start()
    {
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
        if (Score == _maxScore - 1)
        {
            Score++;
            _enemyScoreText.text = $"{_enemyScore} {_maxScore}";
            _isDefeat = true;
        }
        else if (Score < _maxScore - 1)
        {
            Score++;
            _enemyScoreText.text = $"{_enemyScore} {_maxScore}";
        }
    }
}
