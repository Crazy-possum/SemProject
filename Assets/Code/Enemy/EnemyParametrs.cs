using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParametrs : MonoBehaviour  
{
    [Tooltip("Слайдер ХП")]
    [SerializeField] private Slider _healthSlider;
    [Tooltip("Слайдер покраски")] //Нужно будет заменить логику, когда появятся спрайты для покраса
    [SerializeField] private Slider _paintSlider;
    [Tooltip("Максимальное ХП противника")]
    [SerializeField] private float _maxHealth = 1000;
    [Tooltip("Максимальная степень покраски противника")]
    [SerializeField] private float _maxPaintValue = 4;

    private static Action _onEnemyDied;
    private float _currentHealth;
    private float _currentPaintValue;

    public static Action OnEnemyDied { get => _onEnemyDied; set => _onEnemyDied = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float CurrentPaintValue { get => _currentPaintValue; set => _currentPaintValue = value; }

    void Start()
    {
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
            _onEnemyDied?.Invoke();
            Destroy(gameObject);
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
