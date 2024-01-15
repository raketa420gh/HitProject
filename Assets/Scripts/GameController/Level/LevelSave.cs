using System;

[Serializable]
public class LevelSave : ISaveObject
{
    public LevelSlotSaveData[] LevelSlotSaveDatas;

    [NonSerialized] private LevelSelectUISlot[] _levelSlots;

    public LevelSave()
    {
        LevelSlotSaveDatas = null;
    }

    public void Flush()
    {
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