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
}