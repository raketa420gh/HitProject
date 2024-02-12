using System;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour, ILevelController
{
    private ISaveService _saveService;
    private PlayerSave _playerSave;
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
        /*_levelSlots = _uiController.LevelSelectPanel.LevelSelectSlots;

        _playerSave = _saveService.GetSaveObject<PlayerSave>("save");

        if (_playerSave == null)
        {
            foreach (LevelSelectUISlot levelSlot in _levelSlots)
                levelSlot.Reset();
        }
        
        SetLevelNumbers();
        UnlockFirstLevel();*/
    }

    public void InitializeSelectLevelPanel()
    {
        /*int allStarsCount = _levelSlots.Length * 3;
        int currentStarsCount = _lastCompletedLevelNumber * 3;
        _uiController.LevelSelectPanel.SetLevelsInfoText(currentStarsCount, allStarsCount);
        
        _uiController.LevelSelectPanel.OnLevelSelected += HandleLevelSelectEvent;
        _uiController.LevelSelectPanel.Show();
        _uiController.GameModesPanel.Hide();*/
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
        /*_uiController.LevelSelectPanel.LevelSelectSlots[0].SetUnlockState(true);*/
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
}