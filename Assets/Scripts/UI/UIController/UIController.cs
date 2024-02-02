using UnityEngine;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private MainMenuUIPanel _mainMenuPanel;
    [SerializeField] private PlayersInfoUIPanel _playersInfoPanel;
    [SerializeField] private RollDiceUIPanel _rollDicePanel;
    [SerializeField] private CreatePlayerUIPanel _createPlayerPanel;
    [SerializeField] private GameModesUIPanel _gameModesPanel;
    [SerializeField] private InGameUIPanel _inGamePanel;
    [SerializeField] private SelectPlayerTurnUIPanel _selectPlayerTurnPanel;
    [SerializeField] private LevelSelectUIPanel _levelSelectPanel;
    [SerializeField] private LevelCompleteUIPanel _levelCompletePanel;
    [SerializeField] private GameOverUIPanel _gameOverPanel;
    [SerializeField] private SoloGameStageCompleteUIPanel _soloGameStageCompletePanel;
    [SerializeField] private ItemsUIPopup _itemsPopup;
    [SerializeField] private StoreUIPopup _storePopup;
    [SerializeField] private SelectIconUIPanel _selectIconPanel;

    public MainMenuUIPanel MainMenuPanel => _mainMenuPanel;
    public PlayersInfoUIPanel PlayersInfoPanel => _playersInfoPanel;
    public RollDiceUIPanel RollDicePanel => _rollDicePanel;
    public CreatePlayerUIPanel CreatePlayerPanel => _createPlayerPanel;
    public GameModesUIPanel GameModesPanel => _gameModesPanel;
    public InGameUIPanel InGamePanel => _inGamePanel;
    public SelectPlayerTurnUIPanel SelectPlayerTurnPanel => _selectPlayerTurnPanel;
    public LevelSelectUIPanel LevelSelectPanel => _levelSelectPanel;
    public LevelCompleteUIPanel LevelCompletePanel => _levelCompletePanel;
    public GameOverUIPanel GameOverPanel => _gameOverPanel;
    public SoloGameStageCompleteUIPanel SoloGameStageCompletePanel => _soloGameStageCompletePanel;
    public ItemsUIPopup ItemsPopup => _itemsPopup;
    public StoreUIPopup StorePopup => _storePopup;
    public SelectIconUIPanel SelectIconPanel => _selectIconPanel;
}