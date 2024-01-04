using System;

[Serializable]
public class PlayerSave : ISaveObject
{
    public int ID;

    public PlayerSave()
    {
        ID = 0;
    }

    public void Flush()
    {
        
    }
}