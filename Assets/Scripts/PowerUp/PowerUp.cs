using UnityEngine;

[System.Serializable]
public class PowerUp
{
    [SerializeField] private Type _powerUpType;
    [SerializeField] private Sprite _icon;
    private Save _save;   
    
    public Type PowerUpType => _powerUpType;
    public Sprite Icon => _icon;

    public int Amount
    {
        get => _save.Amount;
        set => _save.Amount = value;
    }

    public void SetSave(Save save)
    {
        _save = save;
    }
    
    public enum Type
    {
        Answer50 = 0,
        Time = 1,
        SecondChance = 2
    }

    [System.Serializable]
    public class Save : ISaveObject
    {
        [SerializeField] int _amount;

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public void Flush()
        {
        }
    }
}