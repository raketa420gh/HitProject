public interface IUIController
{
    MainMenuUIPanel MainMenuPanel { get; }
    PlayersInfoUIPanel PlayersInfoPanel { get; }
    RollDiceUIPanel RollDicePanel { get; }
    CreatePlayerUIPanel CreatePlayerPanel { get; }
    GameModesUIPanel GameModesPanel { get; }
    InGameUIPanel InGamePanel { get; }
    LevelCompleteUIPanel LevelCompletePanel { get; }
    GameOverUIPanel GameOverPanel { get; }
}