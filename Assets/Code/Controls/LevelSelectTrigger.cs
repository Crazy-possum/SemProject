using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectTrigger : MonoBehaviour
{
    [SerializeField] private Material _activeTileMaterial;
    [SerializeField] private Material _inactiveTileMaterial;
    [SerializeField] private GameObject _confirmPanel;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private TMP_Text _confirmText;
    [SerializeField] private int _sceneIndex;

    private Timer _confirmDelayTimer;
    private MeshRenderer[] _tileMeshRenderer;
    private bool _isAvailableLevel;
    private bool _inTrigger;
    private bool _isTimer;

    private void Start()
    {
        _confirmDelayTimer = new Timer(1.5f);
        UnlockLevel();
    }

    private void FixedUpdate()
    {
        if (_isTimer)
        {
            RunTimer();
        }
    }

    private void OnEnable()
    {
        TutorStageActivator.OnNeedsLoad += SetPanelActive;
        ProgressSaver.OnSaveNewLevel += UnlockLevel;
    }

    private void OnDisable()
    {
        TutorStageActivator.OnNeedsLoad -= SetPanelActive;
        ProgressSaver.OnSaveNewLevel -= UnlockLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out NavMeshAgent character) && !_inTrigger)
        {
            _inTrigger = true;
            CheckTutor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent character))
        {
            _inTrigger = false;
            _confirmDelayTimer.StopCountdown();
        }
    }

    private void UnlockLevel()
    {
       _tileMeshRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();

        if ((_sceneIndex) <= PlayerPrefs.GetInt("LevelProgress") || (_sceneIndex + 1) <= PlayerPrefs.GetInt("LevelProgress"))
        {
            _isAvailableLevel = true;

            foreach (MeshRenderer mesh in _tileMeshRenderer)
            {
                mesh.material = _activeTileMaterial;
            }
        }
        else
        {
            _isAvailableLevel = false;

            foreach (MeshRenderer mesh in _tileMeshRenderer)
            {
                mesh.material = _inactiveTileMaterial;
            }
        }
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
            if (_isAvailableLevel)
            {
                RunTimer();
            }
        }
    }

    private void RunTimer()
    {
        _confirmDelayTimer.Wait();

        if (!_confirmDelayTimer.StartTimer)
        {
            _confirmDelayTimer.StartCountdown();
            _isTimer = true;
        }

        if (_confirmDelayTimer.ReachingTimerMaxValue == true)
        {
            _isTimer = false;
            _confirmDelayTimer.StopCountdown();

            SetPanelActive(_sceneIndex);
        }
    }

    private void SetPanelActive(int sceneIndex)
    {
        _confirmPanel.SetActive(true);
        _confirmButton.GetComponent<SceneManagerScript>().SceneIndex = sceneIndex;
        Debug.Log(sceneIndex);

        switch (sceneIndex)
        {
            case 2: _confirmText.text = "Загрузить первый уровень"; break;
            case 3: _confirmText.text = "Загрузить второй уровень"; break;
            case 4: _confirmText.text = "Загрузить третий уровень"; break;
            case 5: _confirmText.text = "Загрузить четвертый уровень"; break;
            case 6: _confirmText.text = "Загрузить пятый уровень"; break;
        }
    }
}
