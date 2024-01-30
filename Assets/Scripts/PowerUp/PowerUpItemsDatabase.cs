using UnityEngine;

[CreateAssetMenu(fileName = "PowerUps Database", menuName = "Content/PowerUps/PowerUps Database")]
public class PowerUpItemsDatabase : ScriptableObject
{
    [SerializeField] private PowerUp[] _powerUps;

    public PowerUp[] PowerUps => _powerUps;
}