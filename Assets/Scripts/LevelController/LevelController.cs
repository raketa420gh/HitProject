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
    private GameModeType _currentGameMode;

    public int LastCompletedLevelNumber => _lastCompletedLevelNumber;
    public GameModeType CurrentGameMode => _currentGameMode;

    public event Action<int> OnLevelSelected;
    public event Action<GameModeType> OnRollDicePanelPlayButtonClicked;

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
        _levelSave?.LinkPlayersInfoPanel(_uiController.PlayersInfoPanel);
        
        SetLevelNumbers();
        UnlockFirstLevel();
    }

    public void InitializeSelectLevelPanel()
    {
        int allStarsCount = _levelSlots.Length * 3;
        int currentStarsCount = _lastCompletedLevelNumber * 3;
        _uiController.LevelSelectPanel.SetLevelsInfoText(currentStarsCount, allStarsCount);
        
        _uiController.LevelSelectPanel.OnLevelSelected += HandleLevelSelectEvent;
        _uiController.LevelSelectPanel.Show();
        _uiController.GameModesPanel.Hide();
    }

    public void SetGameMode(GameModeType type)
    {
        _currentGameMode = type;
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

    public void HandleRollDicePlayButtonEvent()
    {
        OnRollDicePanelPlayButtonClicked?.Invoke(_currentGameMode);
    }

    private void HandleLevelSelectEvent(int levelNumber)
    {
        _uiController.LevelSelectPanel.Hide();
        
        OnLevelSelected?.Invoke(levelNumber);
        
        _uiController.LevelSelectPanel.OnLevelSelected -= HandleLevelSelectEvent;
    }
}