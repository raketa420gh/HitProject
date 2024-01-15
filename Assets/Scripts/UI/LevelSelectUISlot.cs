using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUISlot : MonoBehaviour
{
    [SerializeField] private Button _levelSelectButton;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private GameObject _lockObject;
    [SerializeField] private GameObject _starsObject;
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
        
        SetUnlockState(save.IsUnlocked);
        SetCompleteState(save.IsCompleted);
        SetLevelNumber(save.LevelNumber);
        
        UpdateView();
    }

    public LevelSlotSaveData Save()
    {
        return new LevelSlotSaveData(_levelNumber, _isUnlocked, _isCompleted);
    }

    #endregion

    public void Reset()
    {
        SetUnlockState(false);
        SetCompleteState(false);
        SetLevelNumber(0);
        
        UpdateView();
    }

    public void SetUnlockState(bool isUnlocked)
    {
        if (isUnlocked)
            Unlock();
        else
            Lock();
    }

    public void SetCompleteState(bool isCompleted)
    {
        _isCompleted = isCompleted;
        
        UpdateView();
    }

    public void SetLevelNumber(int number)
    {
        _levelNumber = number;
        
        UpdateView();
    }

    private void Unlock()
    {
        _isUnlocked = true;
        
        UpdateView();
    }

    private void Lock()
    {
        _isUnlocked = false;
        
        UpdateView();
    }

    private void UpdateView()
    {
        if (_isUnlocked)
        {
            _lockObject.SetActive(false);
            _levelNumberText.gameObject.SetActive(true);
            _levelSelectButton.interactable = true;
        }
        else
        {
            _lockObject.SetActive(true);
            _levelNumberText.gameObject.SetActive(false);
            _levelSelectButton.interactable = false;
        }

        _levelNumberText.text = _levelNumber.ToString();
        _starsObject.SetActive(_isCompleted);
    }

    private void HandleLevelSelectButtonEvent()
    {
        OnLevelSelectedButtonClicked?.Invoke(_levelNumber);
    }
}