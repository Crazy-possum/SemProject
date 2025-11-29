using System;
using UnityEngine;

public class CharUpgradeViewer : MonoBehaviour
{
    private static Action<bool, float, bool, float, bool, float> _onSubscriptionTower;
    private static Action<bool, float, float> _onSubscriptionCharBullet;

    private bool _isTowerDamageOn;
    private float _towerDamage;

    private bool _isTowerRadiusOn;
    private float _towerRadius;

    private bool _isTowerReloadOn;
    private float _towerReload;

    private bool _isSlowDownOn;
    private float _slowingTimerValue;
    private float _slowingDownValue;

    public static Action<bool, float, bool, float, bool, float> OnSubscriptionTower { get => _onSubscriptionTower; set => _onSubscriptionTower = value; }
    public static Action<bool, float, float> OnSubscriptionCharBullet { get => _onSubscriptionCharBullet; set => _onSubscriptionCharBullet = value; }

    private void OnEnable()
    {
        TowerBuilder.OnBuildTower += SubscriptionTower;

        CharacterUpgrader.OnIncreaseTowerDamage += TowerDamageOn;
        CharacterUpgrader.OnIncreaseTowerRadius += TowerRadiusOn;
        CharacterUpgrader.OnSpeedUpTowerReload += TowerReloadOn;

        CharacterBulletBehavior.OnCollizionEnter += SubscriptionCharacterBullet;

        CharacterUpgrader.OnSlowDownMobs += SlowDownMoveOn;
    }

    private void OnDisable()
    {
        TowerBuilder.OnBuildTower -= SubscriptionTower;

        CharacterUpgrader.OnIncreaseTowerDamage -= TowerDamageOn;
        CharacterUpgrader.OnIncreaseTowerRadius -= TowerRadiusOn;
        CharacterUpgrader.OnSpeedUpTowerReload -= TowerReloadOn;

        CharacterBulletBehavior.OnCollizionEnter -= SubscriptionCharacterBullet;
    }

    #region Tower Action
    private void SubscriptionTower()
    {
        _onSubscriptionTower?.Invoke(_isTowerDamageOn, _towerDamage, _isTowerRadiusOn, _towerRadius, _isTowerReloadOn, _towerReload);
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
        _onSubscriptionCharBullet?.Invoke(_isSlowDownOn, _slowingTimerValue, _slowingDownValue);
    }

    private void SlowDownMoveOn(float debuffTimerValue, float slowingDown)
    {
        _isSlowDownOn = true;
        _slowingTimerValue = debuffTimerValue;
        _slowingDownValue = slowingDown;
    }
    #endregion
}
