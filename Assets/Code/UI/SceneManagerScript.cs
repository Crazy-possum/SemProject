using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private Button _button;

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(CheckTutor);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void CheckTutor()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.StartGame}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.PICK_FIRST_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.PICK_FIRST_LEVEL}");
            TutorController.OnTutorActive?.Invoke();
        }
        else
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
