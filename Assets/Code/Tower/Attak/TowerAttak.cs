using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental;
using static CatapultTowerBehavior;

public class TowerAttak : MonoBehaviour
{
    [Tooltip("Точка, из которой вылетают пули")]
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private GameObject _bulletSpawnerGO;
    [Tooltip("Таймер перезарядки в сек")]

    [SerializeField] private Animator _upgradeAnimator;
    [SerializeField] private GameObject _vfxObject;

    [SerializeField] private Animator _mainAnimator;
    [SerializeField] private Animator _secondaryAnimator;
    [SerializeField] private GameObject _firstUpgradeSprite;
    [SerializeField] private GameObject _secondUpgradeSprite;
    [SerializeField] private GameObject _thirdUpgradeSprite;
    [SerializeField] private GameObject _spriteToInactivate;
    [SerializeField] private GameObject _spriteToInactivate2;

    private float _attakReload;

    private TowerBehavior _towerBehavior;
    private List<GameObject> _targetsList;
    private TowerSO _towerSO;
    private PurchasedUpgrade _purchasedUpgrade;
    private Timer _attakTimer;

    private GameObject _towerBulletPrefab;
    private GameObject _currentTarget;
    private Rigidbody _towerRb;

    private TowerEnum _towerEnum;

    private bool _firstUpgrade;
    private bool _secondUpgrade;
    private bool _thirdUpgrade;
    private bool _firstUpgradeOn;
    private bool _secondUpgradeOn;
    private bool _thirdUpgradeOn;


    public List<GameObject> TargetsList { get => _targetsList; set => _targetsList = value; }
    public TowerSO TowerSO { get => _towerSO; set => _towerSO = value; }
    public GameObject CurrentTarget { get => _currentTarget; set => _currentTarget = value; }
    public float AttakReload { get => _attakReload; set => _attakReload = value; }
    public TowerBehavior TowerBehavior { get => _towerBehavior; set => _towerBehavior = value; }
    public bool FirstUpgrade { get => _firstUpgrade; set => _firstUpgrade = value; }
    public bool SecondUpgrade { get => _secondUpgrade; set => _secondUpgrade = value; }
    public bool ThirdUpgrade { get => _thirdUpgrade; set => _thirdUpgrade = value; }

    private void Start()
    {
        _towerRb = GetComponent<Rigidbody>();
        _towerEnum = _towerSO.TowerEnum;
        _towerBulletPrefab = _towerSO.BulletPrefab;

        SetReloatTimer();

        if (_towerEnum == TowerEnum.Cannon)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
        else if (_towerEnum == TowerEnum.Shotgun)
        {
            _towerBehavior = new ShotgunTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
        else if (_towerEnum == TowerEnum.Catapult)
        {
            _towerBehavior = new CatapultTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }
        else if (_towerEnum == TowerEnum.Sniper)
        {
            _towerBehavior = new SniperTowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }

        if (_towerBehavior == null)
        {
            _towerBehavior = new TowerBehavior(_towerSO, _towerRb, _attakTimer, _towerBulletPrefab, gameObject, _bulletSpawner, _bulletSpawnerGO);
        }

        _towerBehavior.OnTowerShoot += ActivateAnim;
        _towerBehavior.OnSecondTowerShoot += ActivateSecondaryAnim;
    }

    private void FixedUpdate()
    {
        _towerBehavior.SetTarget();
        _towerBehavior.RealoadTimer();

        if (_towerBehavior.TargetsList.Count > 0)
        {
            _towerBehavior.TowerRotate();
        }

        if (_firstUpgrade && !_firstUpgradeOn)
        {
            _firstUpgradeOn = true;
            _firstUpgradeSprite.SetActive(true);

            if (_towerEnum == TowerEnum.Shotgun)
            {
                _spriteToInactivate.SetActive(false);
            }
        }
        if (_secondUpgrade && !_secondUpgradeOn)
        {
            _secondUpgradeOn = true;
            _secondUpgradeSprite.SetActive(true);

            if (_towerEnum == TowerEnum.Catapult)
            {
                _spriteToInactivate.SetActive(false);
            }
            if (_towerEnum == TowerEnum.Shotgun)
            {
                _spriteToInactivate2.SetActive(false);
            }
            if (_towerEnum == TowerEnum.Sniper)
            {
                _spriteToInactivate.SetActive(false);
            }
        }
        if (_thirdUpgrade && !_thirdUpgradeOn)
        {
            _thirdUpgradeOn = true;
            _thirdUpgradeSprite.SetActive(true);

            if (_towerEnum == TowerEnum.Cannon)
            {
                _spriteToInactivate.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        DecisionButton.OnTowerUpgrade += UpgradeVfxOn;

        CharUpgradeViewer.OnSubscriptionTower += ResetNewTowerReloadTimerTime;
        CharacterUpgrader.OnSpeedUpTowerReload += ResetAllTowerReloadTimerTime;

        TowerUpgrader.OnActivateShotgunThirdUpgrade += ResetShotgunReloadTimerTime;
        TowerUpgrader.OnActivateSniperFirstUpgrade += ResetSniperReloadTimerTime;
    }

    private void OnDisable()
    {
        DecisionButton.OnTowerUpgrade -= UpgradeVfxOn;

        CharUpgradeViewer.OnSubscriptionTower -= ResetNewTowerReloadTimerTime;
        CharacterUpgrader.OnSpeedUpTowerReload -= ResetAllTowerReloadTimerTime;

        TowerUpgrader.OnActivateShotgunThirdUpgrade -= ResetShotgunReloadTimerTime;
        TowerUpgrader.OnActivateSniperFirstUpgrade -= ResetSniperReloadTimerTime;

        _towerBehavior.OnTowerShoot -= ActivateAnim;
    }

    public void SetTargetList(List<GameObject> targetsList)
    {
        _towerBehavior.TargetsList = targetsList;
        _currentTarget = null;
        _towerBehavior.CurrentTarget = null;
    }

    public void SetReloatTimer()
    {
        AttakReload = _towerSO.TowerReloadTime;
        _attakTimer = new Timer(AttakReload);
    }

    private void ActivateAnim()
    {
        if (_towerEnum == TowerEnum.Shotgun && _secondUpgrade)
        {
            _secondaryAnimator.Play("Tower_shotgun_2", -1, 0f);
        }
        else
        {
            switch (_towerEnum)
            {
                case TowerEnum.Cannon: _mainAnimator.Play("Tower_cannon", -1, 0f); break;
                case TowerEnum.Shotgun: _mainAnimator.Play("Tower_shotgun", -1, 0f); break;
                case TowerEnum.Catapult: _mainAnimator.Play("Tower_catapult", -1, 0f); break;
                case TowerEnum.Sniper: _mainAnimator.Play("Tower_sniper", -1, 0f); break;
            }
        }
    }

    private void ActivateSecondaryAnim()
    {
        _secondaryAnimator.Play("Tower_cannon_2", -1, 0f);
    }

    //-----------------------Liseners--------------------------------------------------------------------------

    private void UpgradeVfxOn(GameObject tower)
    {
        if (gameObject == tower)
        {
            _vfxObject.SetActive(true);
            _upgradeAnimator.Play("VFX_towerUpgrade", -1, 0f);
        }
    }

    public void ResetShotgunReloadTimerTime(float cutReload, GameObject tower)
    {
        if(_towerEnum == TowerEnum.Shotgun)
        {
            _attakTimer.ResetTimerMaxTime(_attakReload * cutReload);
            _attakReload *= cutReload;
        }
    }

    public void ResetSniperReloadTimerTime(float cutReload, GameObject tower)
    {
        if (_towerEnum == TowerEnum.Sniper)
        {
            _attakTimer.ResetTimerMaxTime(_attakReload * cutReload);
            _attakReload *= cutReload;
        }
    }

    public void ResetNewTowerReloadTimerTime(GameObject towerGO, bool isTowerDamageOn, float towerDamage, bool isTowerRadiusOn, float towerRadius, bool isTowerReloadOn, float towerReload)
    {
        if (isTowerReloadOn)
        {
            if(gameObject == towerGO)
            {
                ResetAllTowerReloadTimerTime(towerReload);
            }
        }
    }

    public void ResetAllTowerReloadTimerTime(float towerReload)
    {
        _attakReload = _attakReload - (_attakReload * towerReload);
        _attakTimer.ResetTimerMaxTime(_attakReload);
        _attakReload *= towerReload;
    }
}

