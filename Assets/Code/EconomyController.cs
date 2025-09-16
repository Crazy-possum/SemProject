using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    [SerializeField] private TMP_Text _currencyText;

    public int GeneralCurrency;
    public int CurrentCost;

    private void Start()
    {
        GeneralCurrency = 10;
        _currencyText.text = GeneralCurrency.ToString();
    }

    public void SpendCurrency()
    {
        GeneralCurrency -= CurrentCost;
        _currencyText.text = GeneralCurrency.ToString();
    }
}
