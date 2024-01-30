using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoftCurrencyUIPanel : UIPanel
{
    [SerializeField] private Image _softCurrencyImage;
    [SerializeField] private TMP_Text _softCurrencyAmountText;
    
    private ICurrenciesController _currenciesController;

    private void OnEnable()
    {
        _currenciesController.OnCurrencyAmountChanged += HandleCurrencyAmountChangeEvent;
        
        SetViewAmount(_currenciesController.Get(Currency.Type.Money));
    }

    private void OnDisable()
    {
        _currenciesController.OnCurrencyAmountChanged -= HandleCurrencyAmountChangeEvent;
    }

    public void Initialize(ICurrenciesController currenciesController)
    {
        _currenciesController = currenciesController;
    }

    private void SetViewAmount(int amount)
    {
        _softCurrencyAmountText.text = amount.ToString();
    }

    private void HandleCurrencyAmountChangeEvent(Currency.Type currencyType, int amount, int amountDifference)
    {
        SetViewAmount(amount);
    }
}