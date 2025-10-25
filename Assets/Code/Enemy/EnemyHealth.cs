using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour  
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _maxHealth = 10;

    public static event Action EnemyDied;

    public float CurrentHealth;

    void Start()
    {
        CurrentHealth = _maxHealth;
        _healthSlider.maxValue = CurrentHealth;
    }

    private void Update()
    {
        _healthSlider.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            EnemyDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
