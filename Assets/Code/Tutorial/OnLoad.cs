using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLoad : MonoBehaviour
{
    [SerializeField] private TutorStageActivator _tutorStageActivator;

    private void Start()
    {
        TutorController.AddListener();

        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
        {
            TutorController.AddLockPlayerPrefs();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 &&
            !PlayerPrefs.HasKey("TutorStage") &&
            !PlayerPrefs.HasKey("IsTutorDone") &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.START_GAME}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.START_GAME}");
            TutorController.OnTutorActive?.Invoke();
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.PickFirstLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.LOAD_FIRST_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.LOAD_FIRST_LEVEL}");
            TutorController.OnTutorActive?.Invoke();
            Debug.Log("rfrf");
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.AllEnemiesKilled}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.SELECT_SECOND_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SELECT_SECOND_LEVEL}");
            TutorController.OnTutorActive?.Invoke();
        }
    }
}
