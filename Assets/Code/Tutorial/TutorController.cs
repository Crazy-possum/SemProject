using System;
using UnityEngine;

public static class TutorController
{
    public static Action<string> OnNewParametr;
    public static Action OnTutorActive;

    public static void AddListener()
    {
        OnNewParametr += UpdateTutor;
    }

    private static void UpdateTutor(string stage)
    {
        if (stage == $"{TutorConstantMaganer.START_GAME}")
        {
            GetTutorStage(TutorEnum.StartGame, false);
            LockTuturialTrigger($"{TutorConstantMaganer.START_GAME}", true);
        }
        else if (stage == $"{TutorConstantMaganer.START_GAME_TRUE}")
        {
            GetTutorStage(TutorEnum.StartGame, true);
            LockTuturialTrigger($"{TutorConstantMaganer.START_GAME_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.PICK_FIRST_LEVEL}")
        {
            GetTutorStage(TutorEnum.PickFirstLevel, false);
            LockTuturialTrigger($"{TutorConstantMaganer.PICK_FIRST_LEVEL}", true);
        }
        else if (stage == $"{TutorConstantMaganer.PICK_FIRST_LEVEL_TRUE}")
        {
            GetTutorStage(TutorEnum.PickFirstLevel, true);
            LockTuturialTrigger($"{TutorConstantMaganer.PICK_FIRST_LEVEL_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.LOAD_FIRST_LEVEL}")
        {
            GetTutorStage(TutorEnum.LoadFirstLevel, false);
            LockTuturialTrigger($"{TutorConstantMaganer.LOAD_FIRST_LEVEL}", true);
        }
        else if (stage == $"{TutorConstantMaganer.LOAD_FIRST_LEVEL_TRUE}")
        {
            GetTutorStage(TutorEnum.LoadFirstLevel, true);
            LockTuturialTrigger($"{TutorConstantMaganer.LOAD_FIRST_LEVEL_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FIRST_ENEMY_HERE}")
        {
            GetTutorStage(TutorEnum.FirstEnemyHere, false);
            LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_HERE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FIRST_ENEMY_HERE_TRUE}")
        {
            GetTutorStage(TutorEnum.FirstEnemyHere, true);
            LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_HERE_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}")
        {
            GetTutorStage(TutorEnum.EnemyNearTowerPoint, false);
            LockTuturialTrigger($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}", true);
        }
        else if (stage == $"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT_TRUE}")
        {
            GetTutorStage(TutorEnum.EnemyNearTowerPoint, true);
            LockTuturialTrigger($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FIRST_TOWER_STRIKE}")
        {
            GetTutorStage(TutorEnum.FirstTowerStrike, false);
            LockTuturialTrigger($"{TutorConstantMaganer.FIRST_TOWER_STRIKE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FIRST_TOWER_STRIKE_TRUE}")
        {
            GetTutorStage(TutorEnum.FirstTowerStrike, true);
            LockTuturialTrigger($"{TutorConstantMaganer.FIRST_TOWER_STRIKE_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FIRST_ENEMY_DIE}")
        {
            GetTutorStage(TutorEnum.FirstEnemyDie, false);
            LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_DIE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FIRST_ENEMY_DIE_TRUE}")
        {
            GetTutorStage(TutorEnum.FirstEnemyDie, true);
            LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_DIE_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.SECOND_TOWER_BUILD}")
        {
            GetTutorStage(TutorEnum.SecondTowerBuild, false);
            LockTuturialTrigger($"{TutorConstantMaganer.SECOND_TOWER_BUILD}", true);
        }
        else if (stage == $"{TutorConstantMaganer.SECOND_TOWER_BUILD_TRUE}")
        {
            GetTutorStage(TutorEnum.SecondTowerBuild, true);
            LockTuturialTrigger($"{TutorConstantMaganer.SECOND_TOWER_BUILD_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.OWN_PLAY}")
        {
            GetTutorStage(TutorEnum.OwnPlay, false);
            LockTuturialTrigger($"{TutorConstantMaganer.OWN_PLAY}", true);
        }
        else if (stage == $"{TutorConstantMaganer.OWN_PLAY_TRUE}")
        {
            GetTutorStage(TutorEnum.OwnPlay, true);
            LockTuturialTrigger($"{TutorConstantMaganer.OWN_PLAY_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.ALL_ENEMIES_KILLED}")
        {
            GetTutorStage(TutorEnum.AllEnemiesKilled, false);
            LockTuturialTrigger($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}", true);
        }
        else if (stage == $"{TutorConstantMaganer.ALL_ENEMIES_KILLED_TRUE}")
        {
            GetTutorStage(TutorEnum.AllEnemiesKilled, true);
            LockTuturialTrigger($"{TutorConstantMaganer.ALL_ENEMIES_KILLED_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.SELECT_SECOND_LEVEL}")
        {
            GetTutorStage(TutorEnum.SelectSecondLevel, false);
            LockTuturialTrigger($"{TutorConstantMaganer.SELECT_SECOND_LEVEL}", true);
        }
        else if (stage == $"{TutorConstantMaganer.SELECT_SECOND_LEVEL_TRUE}")
        {
            GetTutorStage(TutorEnum.SelectSecondLevel, true);
            LockTuturialTrigger($"{TutorConstantMaganer.SELECT_SECOND_LEVEL_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL}")
        {
            GetTutorStage(TutorEnum.TowerInSecondLevel, false);
            LockTuturialTrigger($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL}", true);
        }
        else if (stage == $"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL_TRUE}")
        {
            GetTutorStage(TutorEnum.TowerInSecondLevel, true);
            LockTuturialTrigger($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FINISH_FIRST_WAVE}")
        {
            GetTutorStage(TutorEnum.FinishFirstWave, false);
            LockTuturialTrigger($"{TutorConstantMaganer.FINISH_FIRST_WAVE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FINISH_FIRST_WAVE_TRUE}")
        {
            GetTutorStage(TutorEnum.FinishFirstWave, true);
            LockTuturialTrigger($"{TutorConstantMaganer.FINISH_FIRST_WAVE_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.CAN_UPGRADE}")
        {
            GetTutorStage(TutorEnum.CanUpgrade, false);
            LockTuturialTrigger($"{TutorConstantMaganer.CAN_UPGRADE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.CAN_UPGRADE_TRUE}")
        {
            GetTutorStage(TutorEnum.CanUpgrade, true);
            LockTuturialTrigger($"{TutorConstantMaganer.CAN_UPGRADE_TRUE}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FREE_WAY}")
        {
            GetTutorStage(TutorEnum.FreeWay, false);
            LockTuturialTrigger($"{TutorConstantMaganer.FREE_WAY}", true);
        }
        else if (stage == $"{TutorConstantMaganer.FREE_WAY_TRUE}")
        {
            GetTutorStage(TutorEnum.FreeWay, true);
            LockTuturialTrigger($"{TutorConstantMaganer.FREE_WAY_TRUE}", true);
        }

        PlayerPrefs.Save();
    }

    public static void GetTutorStage(TutorEnum tutorEnum, bool isTutorStageDone)
    {
        PlayerPrefs.SetString("TutorStage", $"{tutorEnum}");
        PlayerPrefs.SetString("IsTutorDone", $"{isTutorStageDone}");
    }

    public static void LockTuturialTrigger(string bollName, bool tutorTriggerValue)
    {
        PlayerPrefs.SetString($"{bollName}", $"{tutorTriggerValue}");
    }

    public static void StopGame()
    {
        Time.timeScale = 0;
    }

    public static void PlayGame()
    {
        Time.timeScale = 1;
    }
    public static void AddLockPlayerPrefs()
    {
        LockTuturialTrigger($"{TutorConstantMaganer.START_GAME}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.START_GAME_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.PICK_FIRST_LEVEL}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.PICK_FIRST_LEVEL_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.LOAD_FIRST_LEVEL}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.LOAD_FIRST_LEVEL_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_HERE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_HERE_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.ENEMY_NEAR_TOWER_POINT_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FIRST_TOWER_STRIKE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FIRST_TOWER_STRIKE_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_DIE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FIRST_ENEMY_DIE_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.SECOND_TOWER_BUILD}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.SECOND_TOWER_BUILD_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.OWN_PLAY}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.OWN_PLAY_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.ALL_ENEMIES_KILLED}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.ALL_ENEMIES_KILLED_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.SELECT_SECOND_LEVEL}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.SELECT_SECOND_LEVEL_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.TOWER_IN_SECOND_LEVEL_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FINISH_FIRST_WAVE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FINISH_FIRST_WAVE_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.CAN_UPGRADE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.CAN_UPGRADE_TRUE}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FREE_WAY}", false);
        LockTuturialTrigger($"{TutorConstantMaganer.FREE_WAY_TRUE}", false);
    }
}
