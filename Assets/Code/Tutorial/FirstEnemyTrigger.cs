using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstEnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("zashol");

        Debug.Log("mob");
        if (SceneManager.GetActiveScene().buildIndex == 2 &&
           PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyHere}" &&
           PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
           PlayerPrefs.GetString($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}") == false.ToString())
        {
            Debug.Log("obnovil");
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}");
            TutorController.OnTutorActive?.Invoke();
        }

        if (other.TryGetComponent(out EnemyMovement enemy))
        {

        }

        gameObject.SetActive(false);
    }
}
