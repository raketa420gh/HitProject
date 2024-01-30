using UnityEngine;
using UnityEngine.UI;

public class ItemsUIPopup : UIPopup
{
    [SerializeField] private SoftCurrencyUIPanel _softCurrencyPanel;
    [SerializeField] private GridLayoutGroup _itemsGrid;
    [SerializeField] private ItemPowerUpUISlot _itemUISlotPrefab;

    public void InitializeCurrencies(ICurrenciesController currenciesController)
    {
        _softCurrencyPanel.Initialize(currenciesController);
    }

    public void InitialisePowerUps(IPowerUpsController powerUpsController)
    {
        PowerUp[] powerUps = powerUpsController.PowerUps;
        
        for (int i = 0; i < powerUps.Length; i++)
        {
            ItemPowerUpUISlot uiItemSlot = Instantiate(_itemUISlotPrefab, _itemsGrid.gameObject.transform);
            uiItemSlot.SetPowerUpView(powerUps[i]);
        }
    }
}