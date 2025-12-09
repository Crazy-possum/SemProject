using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyParametrs : MonoBehaviour  
{
    [SerializeField] private EnemySO _enemySO;
    [Tooltip("Слайдер ХП")]
    [SerializeField] private Slider _healthSlider;
    [Tooltip("Слайдер покраски")] 
    [SerializeField] private Slider _paintSlider;
    [Tooltip("Максимальная степень покраски противника")]
    [SerializeField] private float _maxPaintValue = 4;

    [SerializeField] private GameObject _electroEffectSprite;
    [SerializeField] private GameObject _damageEffectSprite;
    [SerializeField] private GameObject _slowEffectSprite;

    private static Action _onEnemyDied;
    private float _maxHealth;
    private float _currentHealth;
    private float _currentPaintValue;
    private bool _hasDamageWeekness;

    public static Action OnEnemyDied { get => _onEnemyDied; set => _onEnemyDied = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float CurrentPaintValue { get => _currentPaintValue; set => _currentPaintValue = value; }
    public bool HasDamageWeekness { get => _hasDamageWeekness; set => _hasDamageWeekness = value; }
    public EnemySO EnemySO { get => _enemySO; set => _enemySO = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    void Start()
    {
        _maxHealth = _enemySO.MaxHealth;

        _currentHealth = _maxHealth;
        _currentPaintValue = 0;
        _healthSlider.maxValue = _maxHealth;
        _paintSlider.maxValue = _maxPaintValue;
    }

    private void FixedUpdate()
    {
        UpdateHealth();
        UpdatePainting();
    }

    private void UpdateHealth()
    {
        _healthSlider.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        _onEnemyDied?.Invoke();
        Destroy(gameObject);

        if (SceneManager.GetActiveScene().buildIndex == 2 &&
            PlayerPrefs.GetString("TutorStage") == $"{TutorEnum.FirstTowerStrike}" &&
            PlayerPrefs.GetString("IsTutorDone") == true.ToString() &&
            PlayerPrefs.GetString($"{TutorConstantMaganer.FIRST_ENEMY_DIE}") == false.ToString())
        {
            TutorController.OnNewParametr?.Invoke($"{TutorConstantMaganer.FIRST_ENEMY_DIE}");
            TutorController.OnTutorActive?.Invoke();
        }
    }

    private void UpdatePainting()
    {
        if (_currentPaintValue <= _maxPaintValue)
        {
            _paintSlider.value = _currentPaintValue;
        }
    }
}
