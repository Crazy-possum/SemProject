using System;
using UnityEngine;

public class CharUpgradeViewer : MonoBehaviour
{
    private static Action<GameObject, bool, float, bool, float, bool, float> _onSubscriptionTower;
    private static Action<bool, float, float, bool, int> _onSubscriptionCharBullet;
    private static Action<GameObject, bool, float> _onSubscriptionEnemy;

    //---------------------------------------------------------- Переменные для подписки башен
    private bool _isTowerDamageOn;
    private float _towerDamage;

    private bool _isTowerRadiusOn;
    private float _towerRadius;

    private bool _isTowerReloadOn;
    private float _towerReload;

    //---------------------------------------------------------- Переменные для подписки патрона персонажа
    private bool _isSlowDownOn;
    private float _slowingTimerValue;
    private float _slowingDownValue;

    private bool _isDoublePaintOn;
    private int _doublePaintValue;

    //---------------------------------------------------------- Переменные для подписки мобов
    private bool _isSlowMoveOn;
    private float _slowingMoveValue;

    public static Action<GameObject, bool, float, bool, float, bool, float> OnSubscriptionTower { get => _onSubscriptionTower; set => _onSubscriptionTower = value; }
    public static Action<bool, float, float, bool, int> OnSubscriptionCharBullet { get => _onSubscriptionCharBullet; set => _onSubscriptionCharBullet = value; }
    public static Action<GameObject, bool, float> OnSubscriptionEnemy { get => _onSubscriptionEnemy; set => _onSubscriptionEnemy = value; }

    private void OnEnable()
    {
        TowerBuilder.OnBuildTower += SubscriptionTower;
        CharacterUpgrader.OnIncreaseTowerDamage += TowerDamageOn;
        CharacterUpgrader.OnIncreaseTowerRadius += TowerRadiusOn;
        CharacterUpgrader.OnSpeedUpTowerReload += TowerReloadOn;

        CharacterBulletBehavior.OnCollizionEnter += SubscriptionCharacterBullet;
        CharacterUpgrader.OnSlowDownMobs += SlowDownMoveOn;
        CharacterUpgrader.OnDoublePaint += DoublePaintOn;

        EnemySpawner.OnEnemySpawn += SubscriptionEnemy;
        CharacterUpgrader.OnSlowMobsMove += SlowMobsMoveOn;
    }

    private void OnDisable()
    {
        TowerBuilder.OnBuildTower -= SubscriptionTower;
        CharacterUpgrader.OnIncreaseTowerDamage -= TowerDamageOn;
        CharacterUpgrader.OnIncreaseTowerRadius -= TowerRadiusOn;
        CharacterUpgrader.OnSpeedUpTowerReload -= TowerReloadOn;

        CharacterBulletBehavior.OnCollizionEnter -= SubscriptionCharacterBullet;
        CharacterUpgrader.OnSlowDownMobs -= SlowDownMoveOn;
        CharacterUpgrader.OnDoublePaint -= DoublePaintOn;

        EnemySpawner.OnEnemySpawn -= SubscriptionEnemy;
        CharacterUpgrader.OnSlowMobsMove -= SlowMobsMoveOn;
    }

    #region Tower Action
    private void SubscriptionTower(GameObject towerGO)
    {
        _onSubscriptionTower?.Invoke(towerGO, _isTowerDamageOn, _towerDamage, _isTowerRadiusOn, _towerRadius, _isTowerReloadOn, _towerReload);
    }

    private void TowerDamageOn(float towerDamage)
    {
        _isTowerDamageOn = true;
        _towerDamage = towerDamage;
    }

    private void TowerRadiusOn(float towerRange)
    {
        _isTowerRadiusOn = true;
        _towerRadius = towerRange;
    }

    private void TowerReloadOn(float cutTowerReload)
    {
        _isTowerReloadOn = true;
        _towerReload = cutTowerReload;
    }
    #endregion

    #region Character Bullet Action
    private void SubscriptionCharacterBullet()
    {
        _onSubscriptionCharBullet?.Invoke(_isSlowDownOn, _slowingTimerValue, _slowingDownValue, _isDoublePaintOn, _doublePaintValue);
    }

    private void SlowDownMoveOn(float debuffTimerValue, float slowingDown)
    {
        _isSlowDownOn = true;
        _slowingTimerValue = debuffTimerValue;
        _slowingDownValue = slowingDown;
    }

    private void DoublePaintOn(int paintUpValue)
    {
        _isDoublePaintOn = true;
        _doublePaintValue = paintUpValue;
    }
    #endregion

    #region Enemy Action
    private void SubscriptionEnemy(GameObject enemy)
    {
        _onSubscriptionEnemy?.Invoke(enemy, _isSlowMoveOn, _slowingMoveValue);
    }

    private void SlowMobsMoveOn(float slowingDown)
    {
        _isSlowMoveOn = true;
        _slowingMoveValue = slowingDown;
    }
    #endregion
}
