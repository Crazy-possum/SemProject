using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarSprite;

    private Camera _camera;

     void Start()
    {
        _camera = Camera.main;
    }

    public void UpdateHealth(float maxHealth, float currentHealth)
    {
        _healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
