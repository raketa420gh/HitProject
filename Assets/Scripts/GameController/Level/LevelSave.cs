using System;

[Serializable]
public class LevelSave : ISaveObject
{
    public bool IsFirstLoad;
    public LevelSlotSaveData[] LevelSlotSaveDatas;

    [NonSerialized] private LevelSelectUISlot[] _levelSlots;

    public LevelSave()
    {
        LevelSlotSaveDatas = null;
        IsFirstLoad = true;
    }

    public void Flush()
    {
        IsFirstLoad = false;
        
        LevelSlotSaveDatas = new LevelSlotSaveData[_levelSlots.Length];

        for (int i = 0; i < _levelSlots.Length; i++)
        {
            LevelSlotSaveDatas[i] = _levelSlots[i].Save();
        }
    }

    public void LinkLevelSlots(LevelSelectUISlot[] levelSlots)
    {
        _levelSlots = levelSlots;
    }
}