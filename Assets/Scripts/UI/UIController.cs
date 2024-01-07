using UnityEngine;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private PlayersInfoUIPanel _playersInfoPanel;

    public PlayersInfoUIPanel PlayersInfoPanel => _playersInfoPanel;
}