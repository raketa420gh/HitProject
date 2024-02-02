using System;
using System.Linq;
using UnityEngine;

public class SelectIconUIPanel : UIPanel
{
    [SerializeField] private IconUISlot[] _iconSlots;
    
    public event Action<IconUISlot> OnIconSelected;

    public void InitializeSlots()
    {
        int currentNumber = 1;
        
        for (int i = 0; i < _iconSlots.Length; i++)
        {
            _iconSlots[i].SetNumber(currentNumber);
            currentNumber++;
        }
    }

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

    public Sprite GetSprite(int number)
    {
        IconUISlot slotWithSameNumber = _iconSlots.FirstOrDefault(slot => slot.IconNumber == number);

        return slotWithSameNumber == null ? _iconSlots[0].Sprite : slotWithSameNumber.Sprite;
    }

    private void HandleSelectIconButtonClickEvent(IconUISlot iconSlot)
    {
        OnIconSelected?.Invoke(iconSlot);
    }
}