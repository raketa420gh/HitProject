using System;
[Serializable]
public class PlayerSave : ISaveObject
{
    public string PlayerName;

    public PlayerSave()
    {
        PlayerName = null;
    }

    public void Flush()
    {
    }
}