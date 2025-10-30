using TMPro;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    [Tooltip("текст текущей валюты")]
    [SerializeField] private TMP_Text _currencyText;

    public int GeneralCurrency;
    public int CurrentCost;
    public int CurrentIncome;

    private void Start()
    {
        _currencyText.text = GeneralCurrency.ToString();
    }

    private void OnEnable()
    {
        EnemyParametrs.OnEnemyDied += CurrencySum;
    }

    private void OnDisable()
    {
        EnemyParametrs.OnEnemyDied -= CurrencySum;
    }

    private void CurrencySum()
    {
        GeneralCurrency += CurrentIncome;
        _currencyText.text = GeneralCurrency.ToString();
    }

    public void SpendCurrency()
    {
        GeneralCurrency -= CurrentCost;
        _currencyText.text = GeneralCurrency.ToString();
    }
}
