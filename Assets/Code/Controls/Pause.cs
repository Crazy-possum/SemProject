using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private bool isOpened;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpened = !isOpened;
            _pausePanel.SetActive(isOpened);

            if (isOpened)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
