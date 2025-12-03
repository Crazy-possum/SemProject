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

        if (SceneManager.GetActiveScene().buildIndex == 2 && 
            PlayerPrefs.GetString($"{TutorConstantMaganer.START_GAME}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.START_GAME}");
            TutorController.OnTutorActive?.Invoke();
        }
    }
}
