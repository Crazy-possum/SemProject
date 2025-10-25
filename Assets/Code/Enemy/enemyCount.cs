using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyScore;

    private int _maxScore = 5;
    private int _score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyPatrol enemy))
        {
            _score++;
            _enemyScore.text = "¬рагов прошло:" + _score / 2 + "/" + _maxScore;

            Destroy(enemy.gameObject);

            Debug.Log("—чет: " + _score.ToString());
        }
    }
}
