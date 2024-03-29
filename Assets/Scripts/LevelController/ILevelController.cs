using System;

public interface ILevelController
{
    void InitializeLevelSave();
    void InitializeSelectLevelPanel();
    void CompleteLevel(int levelNumber);
    void SetGameMode(GameModeType type);
    void HandleRollDicePlayButtonEvent();
    
    int LastCompletedLevelNumber { get; }
    GameModeType CurrentGameMode { get; }

    event Action<int> OnLevelSelected;
    event Action<GameModeType> OnRollDicePanelPlayButtonClicked;
}