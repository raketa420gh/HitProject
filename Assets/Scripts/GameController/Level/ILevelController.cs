using System;

public interface ILevelController
{
    void InitializeLevelSave();
    void InitializeSelectLevelPanel();
    void CompleteLevel(int levelNumber);

    int LastCompletedLevelNumber { get; }

    event Action<int> OnLevelSelected;
}