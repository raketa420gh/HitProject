using UnityEngine;

public class PlayersInfoUIPanel : UIPanel
{
    [SerializeField] private PlayerInfoUIPanel _youPlayerPanel;
    [SerializeField] private PlayerInfoUIPanel _oppenentPlayerPanel;

    public PlayerInfoUIPanel YouPlayerPanel => _youPlayerPanel;
    public PlayerInfoUIPanel OpponentPlayerPanel => _oppenentPlayerPanel;
}