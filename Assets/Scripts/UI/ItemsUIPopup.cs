using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUIPopup : UIPopup
{
    [SerializeField] private SoftCurrencyUIPanel _softCurrencyPanel;
    [SerializeField] private GridLayoutGroup _itemsGrid;
    [SerializeField] private ItemPowerUpUISlot _itemUISlotPrefab;
    private readonly List<ItemPowerUpUISlot> _uiSlots = new List<ItemPowerUpUISlot>();
    
    private Dictionary<PowerUp.Type, ItemPowerUpUISlot> _activePowerUps = new();

    public ItemPowerUpUISlot[] UiItemSlots => _uiSlots.ToArray();

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
            uiItemSlot.SetUsableState(false);
            
            _uiSlots.Add(uiItemSlot);
            _activePowerUps.Add(powerUps[i].PowerUpType, uiItemSlot);
        }
    }
    
    public void RedrawView(PowerUp.Type type, int amount)
    {
        if (_activePowerUps.ContainsKey(type))
        {
            _activePowerUps[type].SetAmount(amount);
        }
        else
        {
            //ActivateCurrency(type);
        }
    }
}