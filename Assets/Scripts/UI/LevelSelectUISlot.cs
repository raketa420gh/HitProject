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

        if (save.IsUnlocked)
            Unlock();
        else
            Lock();
        
        SetCompleteView(save.IsCompleted);
        
        _levelNumberText.text = _levelNumber.ToString();
    }

    public LevelSlotSaveData Save()
    {
        return new LevelSlotSaveData(_isUnlocked, _isCompleted, _levelNumber);
    }

    #endregion

    public void Lock()
    {
        _isUnlocked = false;
        _lockPanel.Show();
        _levelSelectButton.interactable = false;
    }

    public void Unlock()
    {
        _isUnlocked = true;
        _lockPanel.Hide();
        _levelSelectButton.interactable = true;
    }

    private void SetCompleteView(bool isComplete)
    {
        if (isComplete)
            _starsPanel.Show();
        else
        {
            _starsPanel.Hide();
        }
    }

    private void HandleLevelSelectButtonEvent()
    {
        OnLevelSelectedButtonClicked?.Invoke(_levelNumber);
    }
}