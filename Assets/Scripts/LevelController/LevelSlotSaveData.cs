using UnityEngine;

[System.Serializable]
public class LevelSlotSaveData
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private bool _isCompleted;

    public int LevelNumber => _levelNumber;
    public bool IsUnlocked => _isUnlocked;
    public bool IsCompleted => _isCompleted;

    public LevelSlotSaveData(int levelNumber, bool isUnlocked, bool isCompleted)
    {
        _levelNumber = levelNumber;
        _isUnlocked = isUnlocked;
        _isCompleted = isCompleted;
    }
}