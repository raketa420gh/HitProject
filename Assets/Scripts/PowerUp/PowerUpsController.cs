using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour, IPowerUpsController
{
    [SerializeField] private PowerUpItemsDatabase _powerUpItemsDatabase;
    private PowerUp[] _powerUps;
    private Dictionary<PowerUp.Type, int> _powerUpsLink;
    private ISaveService _saveService;
    private ICurrenciesController _currenciesController;

    public PowerUp[] PowerUps => _powerUps;

    public event Action<PowerUp> OnPowerUpActivated;
    public event Action<PowerUp> OnPowerUpBought;
    public event OnPowerUpAmountChangedCallback OnPowerUpAmountChanged;

    public void Initialize(ICurrenciesController currenciesController)
    {
        _currenciesController = currenciesController;
    }

    public void LoadPowerUps(ISaveService saveService)
    {
        _saveService = saveService;
        
        _powerUps = _powerUpItemsDatabase.PowerUps;
        
        _powerUpsLink = new Dictionary<PowerUp.Type, int>();
        
        for (int i = 0; i < _powerUps.Length; i++)
        {
            if (!_powerUpsLink.ContainsKey(_powerUps[i].PowerUpType))
            {
                _powerUpsLink.Add(_powerUps[i].PowerUpType, i);
            }
            else
            {
                Debug.LogError(string.Format("[PowerUps System]: PowerUp item with type {0} added to database twice!",
                    _powerUps[i].PowerUpType));
            }
            
            PowerUp.Save powerUpSave = _saveService.GetSaveObject<PowerUp.Save>("powerup" + ":" + (int)_powerUps[i].PowerUpType);

            _powerUps[i].SetSave(powerUpSave ?? new PowerUp.Save());
        }
        
        //_itemsPopup.InitialisePowerUps(this);
        
        Enable();
    }

    public void Enable()
    {
        /*foreach (ItemPowerUpUISlot uiSlot in _itemsPopup.UiItemSlots)
        {
            uiSlot.OnBuyButtonClicked += HandleBuyPowerUpEvent;
            uiSlot.OnUseButtonClicked += HandleUsePowerUpEvent;
        }*/
    }

    public void Disable()
    {
        /*foreach (ItemPowerUpUISlot uiSlot in _itemsPopup.UiItemSlots)
        {
            uiSlot.OnBuyButtonClicked -= HandleBuyPowerUpEvent;
            uiSlot.OnUseButtonClicked -= HandleUsePowerUpEvent;
        }*/
    }

    public void SetPowerUpsUsableState(bool isUsable)
    {
        /*for (int i = 0; i < _itemsPopup.UiItemSlots.Length; i++)
        {
            ItemPowerUpUISlot uiItemSlot = _itemsPopup.UiItemSlots[i];
            uiItemSlot.SetUsableState(isUsable);
        }*/
    }
    
    public bool HasAmount(PowerUp.Type powerUpType, int amount)
    {
        return _powerUps[_powerUpsLink[powerUpType]].Amount >= amount;
    }
    
    public void Add(PowerUp.Type powerUpType, int amount, bool redrawUI = true)
    {
        PowerUp powerUp = _powerUps[_powerUpsLink[powerUpType]];

        powerUp.Amount += amount;

        _saveService.ForceSave();

        if (redrawUI)
        {
            //_itemsPopup.RedrawView(powerUpType, powerUp.Amount);
        }
        
        OnPowerUpAmountChanged?.Invoke(powerUpType, powerUp.Amount, amount);
    }
    
    public delegate void OnPowerUpAmountChangedCallback(PowerUp.Type powerUpType, int amount, int amountDifference);

    private void HandleUsePowerUpEvent(PowerUp powerUp)
    {
        if (HasAmount(powerUp.PowerUpType, 1))
        {
            Add(powerUp.PowerUpType, -1);
            
            Debug.Log($"Use power up {powerUp}");
            
            OnPowerUpActivated?.Invoke(powerUp);
        }
    }

    private void HandleBuyPowerUpEvent(PowerUp powerUp)
    {
        if (_currenciesController.HasAmount(Currency.Type.Money, powerUp.Price))
        {
            _currenciesController.Add(Currency.Type.Money, -powerUp.Price);
            Add(powerUp.PowerUpType, 1);
            
            Debug.Log($"Buy power up {powerUp}");
            
            OnPowerUpBought?.Invoke(powerUp);
        }
    }
}