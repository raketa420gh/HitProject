using UnityEngine;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private HudUIPanel _hudPanel;
    [SerializeField] private MainMenuUIPanel _mainMenuPanel;
    [SerializeField] private LevelCompleteUIPanel _levelCompletePanel;
    [SerializeField] private GameOverUIPanel _gameOverPanel;

    public HudUIPanel HudPanel => _hudPanel;
    public MainMenuUIPanel MainMenuPanel => _mainMenuPanel;
    public LevelCompleteUIPanel LevelCompletePanel => _levelCompletePanel;
    public GameOverUIPanel GameOverPanel => _gameOverPanel;
}