using System;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour, ILevelController
{
    private ISaveService _saveService;
    private LevelSave _levelSave;
    private IUIController _uiController;
    private LevelSelectUISlot[] _levelSlots;
    private int _lastCompletedLevelNumber;

    public int LastCompletedLevelNumber => _lastCompletedLevelNumber;

    public event Action<int> OnLevelSelected;

    [Inject]
    public void Construct(ISaveService saveService, IUIController uiController)
    {
        _saveService = saveService;
        _uiController = uiController;
    }

    public void InitializeLevelSave()
    {
        _levelSlots = _uiController.LevelSelectPanel.LevelSelectSlots;

        _levelSave = _saveService.GetSaveObject<LevelSave>("save");

        if (_levelSave == null)
        {
            foreach (LevelSelectUISlot levelSlot in _levelSlots)
                levelSlot.Reset();
        }

        if (_levelSave != null && !_levelSave.LevelSlotSaveDatas.IsNullOrEmpty())
        {
            for (int i = 0; i < _levelSave.LevelSlotSaveDatas.Length; i++)
            {
                _levelSlots[i].Load(_levelSave.LevelSlotSaveDatas[i]);
            }

            _lastCompletedLevelNumber = _levelSave.LastCompletedLevelNumber;
        }
        
        _levelSave?.LinkLevelSlots(_levelSlots);

        SetLevelNumbers();

        //Test
        UnlockFirstLevel();
        _saveService.Save();
    }

    public void InitializeSelectLevelPanel()
    {
        _uiController.LevelSelectPanel.OnLevelSelected += HandleLevelSelectEvent;
        _uiController.LevelSelectPanel.Show();
        _uiController.GameModesPanel.Hide();
    }

    public void CompleteLevel(int levelNumber)
    {
        if (levelNumber > _lastCompletedLevelNumber)
            _lastCompletedLevelNumber = levelNumber;

        _levelSlots[levelNumber - 1]?.SetCompleteState(true);
        _levelSlots[levelNumber]?.SetUnlockState(true);
    }

    private void UnlockFirstLevel()
    {
        _uiController.LevelSelectPanel.LevelSelectSlots[0].SetUnlockState(true);
    }

    private void SetLevelNumbers()
    {
        int levelNumber = 1;

        foreach (var levelSlot in _levelSlots)
        {
            levelSlot.SetLevelNumber(levelNumber);
            levelNumber++;
        }
    }

    private void HandleLevelSelectEvent(int levelNumber)
    {
        _uiController.LevelSelectPanel.Hide();
        
        OnLevelSelected?.Invoke(levelNumber);
        
        _uiController.LevelSelectPanel.OnLevelSelected -= HandleLevelSelectEvent;
    }
}