using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour  
{
    [SerializeField] private float _maxHealth = 10;

    public float CurrentHealth;

    private healthBar _healthBar;

    void Start()
    {
        CurrentHealth = _maxHealth;
        _healthBar = GetComponentInChildren<healthBar>();
    }

    private void Update()
    {
        _healthBar.UpdateHealth(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
