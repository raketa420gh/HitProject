using UnityEngine;

public class ItemsUIPopup : UIPopup
{
    [SerializeField] private SoftCurrencyUIPanel _softCurrencyPanel;

    public void Initialize(ICurrenciesController currenciesController)
    {
        _softCurrencyPanel.Initialize(currenciesController);
    }
}