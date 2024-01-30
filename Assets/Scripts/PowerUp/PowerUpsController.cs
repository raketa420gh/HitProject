using System.Collections.Generic;
using UnityEngine;

public class PowerUpsController : MonoBehaviour, IPowerUpsController
{
    [SerializeField] private PowerUpItemsDatabase _powerUpItemsDatabase;
    [SerializeField] private ItemsUIPopup _itemsPopup;
    private PowerUp[] _powerUps;
    private Dictionary<PowerUp.Type, int> _powerUpsLink;
    private ISaveService _saveService;

    public PowerUp[] PowerUps => _powerUps;

    public void Initialise(ISaveService saveService)
    {
        _saveService = saveService;
        
        _powerUps = _powerUpItemsDatabase.PowerUps;
        
        _powerUpsLink = new Dictionary<PowerUp.Type, int>();
        
        for (int i = 0; i < _powerUps.Length; i++)
        {
            if (!_powerUpsLink.ContainsKey(_powerUps[i].PowerUpType))
            {
                _powerUpsLink.Add(_powerUps[i].PowerUpType, i);
            }
            else
            {
                Debug.LogError(string.Format("[PowerUps System]: PowerUp item with type {0} added to database twice!",
                    _powerUps[i].PowerUpType));
            }
            
            PowerUp.Save powerUpSave = _saveService.GetSaveObject<PowerUp.Save>("powerup" + ":" + (int)_powerUps[i].PowerUpType);

            _powerUps[i].SetSave(powerUpSave ?? new PowerUp.Save());
        }
        
        _itemsPopup.InitialisePowerUps(this);
    }
}