using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstEnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement enemy))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 &&
               PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyHere}" &&
               PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
               PlayerPrefs.GetString($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}") == false.ToString())
            {
                TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}");
                TutorController.OnTutorActive?.Invoke();
            }
        }

        gameObject.SetActive(false);
    }
}
