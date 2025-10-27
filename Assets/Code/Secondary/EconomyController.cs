using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyText;

    public int GeneralCurrency;
    public int CurrentCost;
    public int CurrentIncome;

    private void Start()
    {
        CurrentIncome = 1;
        GeneralCurrency = 10;
        _currencyText.text = GeneralCurrency.ToString();
    }

    private void OnEnable()
    {
        EnemyParametrs.EnemyDied += CurrencySum;
    }

    private void OnDisable()
    {
        EnemyParametrs.EnemyDied -= CurrencySum;
    }

    private void CurrencySum()
    {
        GeneralCurrency = GeneralCurrency + CurrentIncome;
        _currencyText.text = GeneralCurrency.ToString();
    }

    public void SpendCurrency()
    {
        GeneralCurrency -= CurrentCost;
        _currencyText.text = GeneralCurrency.ToString();
    }
}
