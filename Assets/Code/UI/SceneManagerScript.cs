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

    private void OnEnable()
    {
        TutorStageActivator.OnNeedsLoad += LoadScene;
    }

    private void OnDisable()
    {
        TutorStageActivator.OnNeedsLoad -= LoadScene;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void CheckTutor()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.StartGame}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.PICK_FIRST_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.PICK_FIRST_LEVEL}");
            TutorController.OnTutorActive?.Invoke();
        }
        else
        {
            LoadScene(_sceneIndex);
        }
    }

    private void LoadScene(int index)
    {
        Debug.Log(index);
        SceneManager.LoadScene(index);
    }
}
