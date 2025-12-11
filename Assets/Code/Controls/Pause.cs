using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private Button _pauseButton;
    private bool _canOpen = true;

    public bool CanOpen { get => _canOpen; set => _canOpen = value; }

    private void Start()
    {
        _pauseButton = GetComponent<Button>();
        _pauseButton.onClick.AddListener(SetPause);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pausePanel.SetActive(_canOpen);

            if (_canOpen)
            {
                TutorController.StopGame();
                _canOpen = false;
            }
            else
            {
                TutorController.PlayGame();
                _canOpen = true;
            }
        }
    }

    private void SetPause()
    {
        _pausePanel.SetActive(_canOpen);

        if (_canOpen)
        {
            TutorController.StopGame();
            _canOpen = false;
        }
        else
        {
            TutorController.PlayGame();
            _canOpen = true;
        }
    }
}
