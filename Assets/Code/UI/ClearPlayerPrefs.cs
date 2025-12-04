using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPlayerPrefs : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClearPP);
    }

    private void ClearPP()
    {
        PlayerPrefs.DeleteAll();
    }
}
