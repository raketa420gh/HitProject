using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUIPanel : UIPanel
{
    [SerializeField] private LevelSelectUISlot[] _levelSelectSlots;
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _levelsInfoText;

    public LevelSelectUISlot[] LevelSelectSlots => _levelSelectSlots;

    public event Action<int> OnLevelSelected;
    public event Action OnBackButtonClick;

    private void OnEnable()
    {
        foreach (LevelSelectUISlot levelSelectButton in _levelSelectSlots)
        {
            levelSelectButton.OnLevelSelectedButtonClicked += HandleLevelSelectButtonClickEvent;
        }
        
        _backButton.onClick.AddListener(HandleBackButtonClickEvent);
    }

    private void OnDisable()
    {
        foreach (LevelSelectUISlot levelSelectButton in _levelSelectSlots)
        {
            levelSelectButton.OnLevelSelectedButtonClicked -= HandleLevelSelectButtonClickEvent;
        }
        
        _backButton.onClick.RemoveListener(HandleBackButtonClickEvent);
    }

    public void SetLevelsInfoText(int currentStarsCount, int allStarsCount)
    {
        _levelsInfoText.text = $"{currentStarsCount} <size=42><color=#9399a3>/ {allStarsCount}</size></color>";
    }

    private void HandleLevelSelectButtonClickEvent(int levelNumber)
    {
        Debug.Log($"Level select. Number = {levelNumber}");
        OnLevelSelected?.Invoke(levelNumber);
    }

    private void HandleBackButtonClickEvent()
    {
        OnBackButtonClick?.Invoke();
    }
}