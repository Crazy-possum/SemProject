using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorStageActivator : MonoBehaviour
{
    [SerializeField] private GameObject _tutorPanel;
    [SerializeField] private GameObject _darkPanel;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private TMP_Text _textCharacter;
    [SerializeField] private TMP_Text _textNPC;
    [SerializeField] private Image _imageCharacter;
    [SerializeField] private Image _imageCharacterLocker;
    [SerializeField] private Image _imageNPC;
    [SerializeField] private Image _imageNPCLocker;

    private List<TutorStageSO> _tutorStageSOList;
    private TutorStageSO _tutorStageSO;
    private TutorDialogSO _tutorDialogSO;
    private int _currentDialogIndex;

    private Vector3 _charImagePos;
    private Vector3 _NPCImagePos;
    private Vector3 _imageShift = new Vector3(0, 35, 0);

    private static Action<int> _onNeedsLoad;
    private static Action _onNeedsWin;

    public static Action<int> OnNeedsLoad { get => _onNeedsLoad; set => _onNeedsLoad = value; }
    public static Action OnNeedsWin { get => _onNeedsWin; set => _onNeedsWin = value; }

    private void Awake()
    {
        LoadTutorStage();
        GetImageVector();
    }

    private void Start()
    {
        _buttonNext.onClick.AddListener(NextButton);
    }

    private void OnEnable()
    {
        TutorController.OnTutorActive += SetTutorialConfig;
    }

    private void OnDisable()
    {
        TutorController.OnTutorActive -= SetTutorialConfig;
    }

    private void FixedUpdate()
    {
        Debug.Log(PlayerPrefs.GetString("TutorStage"));
        Debug.Log(PlayerPrefs.GetString("IsTutorDone"));
    }

    public void SetActivePanel()
    {
        _tutorPanel.SetActive(true);
        _darkPanel.SetActive(true);

        TutorController.StopGame();
        CustomizePanel();
    }

    private void CustomizePanel()
    {
        if (_tutorDialogSO.IsCharacterSpeak)
        {
            _textCharacter.text = _tutorDialogSO.CharacterText;
            _textNPC.text = _tutorDialogSO.NPCText;

            SetMainImage(_imageCharacter, _imageCharacterLocker, _charImagePos, _imageNPC, _imageNPCLocker, _NPCImagePos);
        }
        else
        {
            _textNPC.text = _tutorDialogSO.NPCText;
            _textCharacter.text = _tutorDialogSO.CharacterText;

            SetMainImage(_imageNPC, _imageNPCLocker, _NPCImagePos, _imageCharacter, _imageCharacterLocker, _charImagePos);
        }

        _imageCharacter.sprite = _tutorDialogSO.CharacterSprite;
        _imageNPC.sprite = _tutorDialogSO.NPCSprite;
    }

    private void SetMainImage(Image mainImage, Image mainImageLocker, Vector3 mainImagePos, Image secondImage, Image secondImageLocker, Vector3 secondImagePos)
    {
        mainImageLocker.gameObject.SetActive(false);
        secondImageLocker.gameObject.SetActive(true);

        mainImage.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        mainImage.rectTransform.localPosition = mainImagePos + _imageShift;

        secondImage.transform.localScale = new Vector3(1f, 1f, 1f);
        secondImage.rectTransform.localPosition = secondImagePos;
    }

    private void SetTutorialConfig()
    {
        string tutorStage = PlayerPrefs.GetString("TutorStage");
        foreach (var stage in _tutorStageSOList)
        {
            if (stage.TutorEnum.ToString() == tutorStage)
            {
                _tutorStageSO = stage;
                break;
            }
        }

        _currentDialogIndex = 0;
        _tutorDialogSO = _tutorStageSO.TutorDialogSOList[_currentDialogIndex];
        SetActivePanel();
    }

    private void NextButton()
    {
        _currentDialogIndex++;

        if (_currentDialogIndex < _tutorStageSO.TutorDialogSOList.Count)
        {
            _tutorDialogSO = _tutorStageSO.TutorDialogSOList[_currentDialogIndex];
            CustomizePanel();
        }
        else
        {
            CompleteTutorStage();
        }
    }

    private void CompleteTutorStage()
    {
        TutorController.PlayGame();
        _tutorPanel.SetActive(false);
        _darkPanel.SetActive(false);

        ChangeTutorStageToComplite();
    }

    private void ChangeTutorStageToComplite()
    {
        string tutorStage = PlayerPrefs.GetString("TutorStage");
        string isTutorDone = PlayerPrefs.GetString("IsTutorDone");

        if (tutorStage == $"{TutorEnum.StartGame}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.START_GAME_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.PickFirstLevel}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.PICK_FIRST_LEVEL_TRUE}");
            _onNeedsLoad?.Invoke(2);
        }
        else if (tutorStage == $"{TutorEnum.LoadFirstLevel}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.LOAD_FIRST_LEVEL_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.FirstEnemyHere}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_HERE_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.EnemyNearTowerPoint}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.FirstTowerStrike}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_TOWER_STRIKE_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.FirstEnemyDie}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_DIE_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.SecondTowerBuild}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SECOND_TOWER_BUILD_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.OwnPlay}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.OWN_PLAY_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.AllEnemiesKilled}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ALL_ENEMIES_KILLED_TRUE}");
            _onNeedsWin?.Invoke();
        }
        else if (tutorStage == $"{TutorEnum.SelectSecondLevel}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SELECT_SECOND_LEVEL_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.TowerInSecondLevel}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.FinishFirstWave}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FINISH_FIRST_WAVE_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.CanUpgrade}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.CAN_UPGRADE_TRUE}");
        }
        else if (tutorStage == $"{TutorEnum.FreeWay}" && isTutorDone == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FREE_WAY_TRUE}");
        }
    }

    private void LoadTutorStage()
    {
        _tutorStageSOList = new List<TutorStageSO>();

        var tutNames = new[] 
            {"StartGame", "PickFirstLevel", "LoadFirstLevel", "FirstEnemyHere", 
            "EnemyNearTowerPoint", "FirstTowerStrike", "FirstEnemyDie", 
            "SecondTowerBuild", "OwnPlay", "AllEnemiesKilled", "SelectSecondLevel",
            "TowerInSecondLevel", "FinishFirstWave", "CanUpgrade", "FreeWay"};

        for (int i = 0; i < tutNames.Length; i++)
        {
            _tutorStageSOList.Add(Resources.Load<TutorStageSO>($"Tutorial/Tut_{i+1}_{tutNames[i]}"));
        }
    }

    private void GetImageVector()
    {
        _charImagePos = _imageCharacter.rectTransform.localPosition;
        _NPCImagePos = _imageNPC.rectTransform.localPosition;
    }
}
