using System;

public interface IPowerUpsController
{
    PowerUp[] PowerUps { get; }
    
    event Action<PowerUp> OnPowerUpActivated;
    event Action<PowerUp> OnPowerUpBought;

    void Enable();
    void Disable();
    void Initialize(ICurrenciesController currenciesController);
    void LoadPowerUps(ISaveService saveService);
    void SetPowerUpsUsableState(bool isUsable);
    bool HasAmount(PowerUp.Type powerUpType, int amount);
    void Add(PowerUp.Type powerUpType, int amount, bool redrawUI = true);
}