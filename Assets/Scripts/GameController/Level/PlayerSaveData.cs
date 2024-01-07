using System;

[Serializable]
public class PlayerSaveData : ISaveObject
{
    public string Name;

    public PlayerSaveData()
    {
        Name = "DefaultPlayer";
    }

    public void Flush()
    {
        
    }
}