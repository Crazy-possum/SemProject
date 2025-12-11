using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Pause _pause;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        _panel.SetActive(false);
        Time.timeScale = 1.0f;

        if (_pause != null)
        {
            _pause.CanOpen = true;
        }
    }
}
