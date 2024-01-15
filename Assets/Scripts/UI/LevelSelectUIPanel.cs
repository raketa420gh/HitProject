using System;
using UnityEngine;

public class LevelSelectUIPanel : UIPanel
{
    [SerializeField] private LevelSelectUISlot[] _levelSelectSlots;

    public LevelSelectUISlot[] LevelSelectSlots => _levelSelectSlots;

    public event Action<int> OnLevelSelected;

    private void OnEnable()
    {
        foreach (LevelSelectUISlot levelSelectButton in _levelSelectSlots)
        {
            levelSelectButton.OnLevelSelectedButtonClicked += HandleLevelSelectButtonClickEvent;
        }
    }

    private void OnDisable()
    {
        foreach (LevelSelectUISlot levelSelectButton in _levelSelectSlots)
        {
            levelSelectButton.OnLevelSelectedButtonClicked -= HandleLevelSelectButtonClickEvent;
        }
    }

    private void HandleLevelSelectButtonClickEvent(int levelNumber)
    {
        Debug.Log($"Level select. Number = {levelNumber}");
        OnLevelSelected?.Invoke(levelNumber);
    }
}