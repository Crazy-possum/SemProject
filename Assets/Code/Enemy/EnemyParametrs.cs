using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParametrs : MonoBehaviour  
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _paintSlider;
    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private float _maxPaint = 4;

    public static event Action EnemyDied;

    public float CurrentHealth;
    public float CurrentPaint;

    void Start()
    {
        CurrentHealth = _maxHealth;
        CurrentPaint = 0;
        _healthSlider.maxValue = _maxHealth;
        _paintSlider.maxValue = _maxPaint;
    }

    private void Update()
    {
        UpdateHealth();
        UpdatePainting();
    }

    private void UpdateHealth()
    {
        _healthSlider.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            EnemyDied?.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdatePainting()
    {
        if (CurrentPaint < 4)
        {
            _paintSlider.value = CurrentPaint;
        }
    }
}
