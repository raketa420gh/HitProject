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

    public void Reset()
    {
        SetUnlockState(false);
        SetCompleteState(false);
        SetLevelNumber(0);
    }

    #region Load/Save

    public void Load(LevelSlotSaveData save)
    {
        _isUnlocked = save.IsUnlocked;
        _isCompleted = save.IsCompleted;
        _levelNumber = save.LevelNumber;
        
        SetUnlockState(save.IsUnlocked);
        SetCompleteState(save.IsCompleted);
        SetLevelNumber(save.LevelNumber);
    }

    public LevelSlotSaveData Save()
    {
        return new LevelSlotSaveData(_levelNumber, _isUnlocked, _isCompleted);
    }

    #endregion

    public void SetUnlockState(bool isUnlocked)
    {
        if (isUnlocked)
            Unlock();
        else
            Lock();
    }

    public void SetCompleteState(bool isComplete)
    {
        if (isComplete)
            _starsPanel.Show();
        else
        {
            _starsPanel.Hide();
        }
    }

    public void SetLevelNumber(int number)
    {
        _levelNumberText.text = number.ToString();
    }

    private void Unlock()
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

    private void HandleLevelSelectButtonEvent()
    {
        OnLevelSelectedButtonClicked?.Invoke(_levelNumber);
    }
}