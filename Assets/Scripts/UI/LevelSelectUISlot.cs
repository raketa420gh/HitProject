using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUISlot : MonoBehaviour
{
    [SerializeField] private Button _levelSelectButton;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private UIPanel _lockPanel;
    [SerializeField] private UIPanel _starsPanel;
    private bool _isUnlocked;
    private bool _isCompleted;
    private int _levelNumber;

    public event Action<int> OnLevelSelectedButtonClicked;

    private void OnEnable()
    {
        _levelSelectButton.onClick.AddListener(HandleLevelSelectButtonEvent);
    }

    private void OnDisable()
    {
        _levelSelectButton.onClick.RemoveListener(HandleLevelSelectButtonEvent);
    }

    #region Load/Save

    public void Load(LevelSlotSaveData save)
    {
        _isUnlocked = save.IsUnlocked;
        _isCompleted = save.IsCompleted;
        _levelNumber = save.LevelNumber;
        
        SetLockState(save.IsUnlocked);
        SetCompleteState(save.IsCompleted);
        _levelNumberText.text = save.LevelNumber.ToString();
    }

    public LevelSlotSaveData Save()
    {
        return new LevelSlotSaveData(_isUnlocked, _isCompleted, _levelNumber);
    }

    #endregion

    public void Unlock()
    {
        _isUnlocked = true;
        _lockPanel.Hide();
        _levelSelectButton.interactable = true;
    }

    private void Lock()
    {
        _isUnlocked = false;
        _lockPanel.Show();
        _levelSelectButton.interactable = false;
    }

    private void SetCompleteState(bool isComplete)
    {
        if (isComplete)
            _starsPanel.Show();
        else
        {
            _starsPanel.Hide();
        }
    }

    private void SetLockState(bool isUnlocked)
    {
        if (isUnlocked)
            Unlock();
        else
            Lock();
    }
    
    private void HandleLevelSelectButtonEvent()
    {
        OnLevelSelectedButtonClicked?.Invoke(_levelNumber);
    }
}