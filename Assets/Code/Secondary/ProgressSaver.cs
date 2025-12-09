using System;
using System.Reflection;
using UnityEngine;

public class ProgressSaver : MonoBehaviour
{
    private static Action _onSaveNewLevel;

    public static Action OnSaveNewLevel { get => _onSaveNewLevel; set => _onSaveNewLevel = value; }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("LevelProgress"))
        {
            PlayerPrefs.SetInt("LevelProgress", 2);
            PlayerPrefs.Save();
        }
    }

    private void OnEnable()
    {
        WinLoseController.OnCompliteLevel += SaveProgress;
    }

    private void OnDisable()
    {
        WinLoseController.OnCompliteLevel -= SaveProgress;
    }

    private void SaveProgress(int index)
    {
        if (index > PlayerPrefs.GetInt("LevelProgress"))
        {
            Debug.Log("zapis");
            PlayerPrefs.SetInt("LevelProgress", index);
            PlayerPrefs.Save();
            _onSaveNewLevel.Invoke();
        }
    }
}
