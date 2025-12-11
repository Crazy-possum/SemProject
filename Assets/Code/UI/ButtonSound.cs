using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _clickSource;
    [SerializeField] private AudioClip _click;
    private Button _button;

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(PlaySound);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void PlaySound()
    {
        _clickSource.PlayOneShot(_click);
    }
}
