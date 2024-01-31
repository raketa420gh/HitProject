using System;

public interface IPowerUpsController
{
    PowerUp[] PowerUps { get; }
    
    event Action<PowerUp.Type> OnPowerUpActivated;
    event Action<PowerUp.Type> OnPowerUpBought;

    void Enable();
    void Disable();
    void LoadPowerUps(ISaveService saveService);
    void SetPowerUpsUsableState(bool isUsable);
}