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

    public LevelSlotSaveData(bool isUnlocked, bool isCompleted, int levelNumber)
    {
        _isUnlocked = isUnlocked;
        _isCompleted = isCompleted;
        _levelNumber = levelNumber;
    }
}