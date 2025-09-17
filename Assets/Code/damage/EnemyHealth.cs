using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour  
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _maxHealth = 10;

    public float CurrentHealth;

    private healthBar _healthBar;

    void Start()
    {
        CurrentHealth = _maxHealth;
        _healthSlider.maxValue = CurrentHealth;
        _healthBar = GetComponentInChildren<healthBar>();
    }

    private void Update()
    {
        _healthSlider.value = CurrentHealth;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
