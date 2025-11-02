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
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
