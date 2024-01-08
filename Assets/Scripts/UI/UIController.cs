using UnityEngine;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private MainMenuUIPanel _mainMenuPanel;
    [SerializeField] private PlayersInfoUIPanel _playersInfoPanel;
    [SerializeField] private RollDiceUIPanel _rollDicePanel;
    [SerializeField] private CreatePlayerUIPanel _createPlayerPanel;
    [SerializeField] private GameModesUIPanel _gameModesPanel;
    [SerializeField] private InGameUIPanel _inGamePanel;

    public MainMenuUIPanel MainMenuPanel => _mainMenuPanel;
    public PlayersInfoUIPanel PlayersInfoPanel => _playersInfoPanel;
    public RollDiceUIPanel RollDicePanel => _rollDicePanel;
    public CreatePlayerUIPanel CreatePlayerPanel => _createPlayerPanel;
    public GameModesUIPanel GameModesPanel => _gameModesPanel;
    public InGameUIPanel InGamePanel => _inGamePanel;

}