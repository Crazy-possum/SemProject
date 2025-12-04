using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //private static Action

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
        mainImage.transform.position = mainImagePos + _imageShift;

        secondImage.transform.localScale = new Vector3(1f, 1f, 1f);
        secondImage.transform.position = secondImagePos;
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

        TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.START_GAME_TRUE}");
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
        _charImagePos = _imageCharacter.transform.position;
        _NPCImagePos = _imageNPC.transform.position;
    }

    private void SetConfig(string tutorStage, string boolName)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.StartGame}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.START_GAME_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.START_GAME_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.StartGame}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.PICK_FIRST_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.PICK_FIRST_LEVEL}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.PickFirstLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.PICK_FIRST_LEVEL_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.PICK_FIRST_LEVEL_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.PickFirstLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.LOAD_FIRST_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.LOAD_FIRST_LEVEL}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.LoadFirstLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.LOAD_FIRST_LEVEL_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.LOAD_FIRST_LEVEL_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.LoadFirstLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_ENEMY_HERE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_HERE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyHere}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_ENEMY_HERE_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_HERE_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyHere}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.EnemyNearTowerPoint}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.EnemyNearTowerPoint}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_TOWER_STRIKE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_TOWER_STRIKE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstTowerStrike}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_TOWER_STRIKE_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_TOWER_STRIKE_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstTowerStrike}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_ENEMY_DIE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_DIE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyDie}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_ENEMY_DIE_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_DIE_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstEnemyDie}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.SECOND_TOWER_BUILD}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SECOND_TOWER_BUILD}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.SecondTowerBuild}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.SECOND_TOWER_BUILD_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SECOND_TOWER_BUILD_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.SecondTowerBuild}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.OWN_PLAY}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.OWN_PLAY}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.OwnPlay}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.OWN_PLAY_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.OWN_PLAY_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.OwnPlay}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.AllEnemiesKilled}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.ALL_ENEMIES_KILLED_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.ALL_ENEMIES_KILLED_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.AllEnemiesKilled}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.SELECT_SECOND_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SELECT_SECOND_LEVEL}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.SelectSecondLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.SELECT_SECOND_LEVEL_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.SELECT_SECOND_LEVEL_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.SelectSecondLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.TowerInSecondLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.TowerInSecondLevel}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FINISH_FIRST_WAVE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FINISH_FIRST_WAVE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FinishFirstWave}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FINISH_FIRST_WAVE_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FINISH_FIRST_WAVE_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FinishFirstWave}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.CAN_UPGRADE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.CAN_UPGRADE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.CanUpgrade}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.CAN_UPGRADE_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.CAN_UPGRADE_TRUE}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.CanUpgrade}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FREE_WAY}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FREE_WAY}");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FreeWay}" &&
            PlayerPrefs.GetString("IsTutorDone") == false.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FREE_WAY_TRUE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FREE_WAY_TRUE}");
        }
    }
}
