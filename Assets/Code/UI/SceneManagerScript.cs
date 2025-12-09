using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private Button _button;

    public int SceneIndex { get => _sceneIndex; set => _sceneIndex = value; }

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(LoadOnButton);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void LoadOnButton()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
