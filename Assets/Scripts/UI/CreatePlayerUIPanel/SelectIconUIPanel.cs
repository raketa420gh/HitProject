using System;
using UnityEngine;

public class SelectIconUIPanel : UIPanel
{
    [SerializeField] private IconUISlot[] _iconSlots;
    
    public event Action<IconUISlot> OnIconSelected;

    private void OnEnable()
    {
        foreach (var iconSlot in _iconSlots)
        {
            iconSlot.OnSelectIconButtonClicked += HandleSelectIconButtonClickEvent;
        }
    }
    
    private void OnDisable()
    {
        foreach (var iconSlot in _iconSlots)
        {
            iconSlot.OnSelectIconButtonClicked -= HandleSelectIconButtonClickEvent;
        }
    }

    private void HandleSelectIconButtonClickEvent(IconUISlot iconSlot)
    {
        OnIconSelected?.Invoke(iconSlot);
    }

    
}