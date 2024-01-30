using UnityEngine;

public class StoreUIPopup : UIPopup
{
    [SerializeField] private SoftCurrencyUIPanel _softCurrencyPanel;

    public void Initialize(ICurrenciesController currenciesController)
    {
        _softCurrencyPanel.Initialize(currenciesController);
    }
}