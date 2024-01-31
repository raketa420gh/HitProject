using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour, IPowerUpsController
{
    [SerializeField] private PowerUpItemsDatabase _powerUpItemsDatabase;
    [SerializeField] private ItemsUIPopup _itemsPopup;
    private PowerUp[] _powerUps;
    private Dictionary<PowerUp.Type, int> _powerUpsLink;
    private ISaveService _saveService;

    public PowerUp[] PowerUps => _powerUps;

    public event Action<PowerUp.Type> OnPowerUpActivated;
    public event Action<PowerUp.Type> OnPowerUpBought;

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
        
        _itemsPopup.InitialisePowerUps(this);
        
        Enable();
    }

    public void Enable()
    {
        foreach (ItemPowerUpUISlot uiSlot in _itemsPopup.UiItemSlots)
        {
            uiSlot.OnBuyButtonClicked += HandleBuyPowerUpEvent;
            uiSlot.OnUseButtonClicked += HandleUsePowerUpEvent;
        }
    }

    public void Disable()
    {
        foreach (ItemPowerUpUISlot uiSlot in _itemsPopup.UiItemSlots)
        {
            uiSlot.OnBuyButtonClicked -= HandleBuyPowerUpEvent;
            uiSlot.OnUseButtonClicked -= HandleUsePowerUpEvent;
        }
    }

    public void SetPowerUpsUsableState(bool isUsable)
    {
        for (int i = 0; i < _itemsPopup.UiItemSlots.Length; i++)
        {
            ItemPowerUpUISlot uiItemSlot = _itemsPopup.UiItemSlots[i];
            uiItemSlot.SetUsableState(isUsable);
        }
    }

    private void HandleUsePowerUpEvent(PowerUp.Type type)
    {
        Debug.Log($"Use power up {type}");
        
        OnPowerUpActivated?.Invoke(type);
    }

    private void HandleBuyPowerUpEvent(PowerUp.Type type)
    {
        Debug.Log($"Buy power up {type}");
        
        OnPowerUpBought?.Invoke(type);
    }
}