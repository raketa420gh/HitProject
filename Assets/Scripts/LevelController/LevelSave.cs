using System;
using System.Linq;

[Serializable]
public class LevelSave : ISaveObject
{
    public string PlayerName;
    public int LastCompletedLevelNumber;
    public LevelSlotSaveData[] LevelSlotSaveDatas;

    [NonSerialized] private LevelSelectUISlot[] _levelSlots;
    [NonSerialized] private PlayersInfoUIPanel _playersInfoPanel;

    public LevelSave()
    {
        PlayerName = null;
        LevelSlotSaveDatas = null;
    }

    public void Flush()
    {
        LevelSlotSaveDatas = new LevelSlotSaveData[_levelSlots.Length];

        for (int i = 0; i < _levelSlots.Length; i++)
        {
            LevelSlotSaveDatas[i] = _levelSlots[i].Save();
        }

        LevelSlotSaveData[] completedLevels = LevelSlotSaveDatas.Where(levelSlotSaveData => levelSlotSaveData.IsCompleted).ToArray();

        if (completedLevels is { Length: > 0 })
        {
            LevelSlotSaveData maxLevelNumberData = completedLevels.OrderByDescending(levelSlotSaveData => levelSlotSaveData.LevelNumber).FirstOrDefault();
            LastCompletedLevelNumber = maxLevelNumberData != null ? maxLevelNumberData!.LevelNumber : 0;
        }

        PlayerName = _playersInfoPanel.YouPlayerPanel.PlayerName;
    }

    public void LinkLevelSlots(LevelSelectUISlot[] levelSlots)
    {
        _levelSlots = levelSlots;
    }

    public void LinkPlayersInfoPanel(PlayersInfoUIPanel playersInfoPanel)
    {
        _playersInfoPanel = playersInfoPanel;
    }
}