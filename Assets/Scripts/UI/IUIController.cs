public interface IUIController
{
    MainMenuUIPanel MainMenuPanel { get; }
    PlayersInfoUIPanel PlayersInfoPanel { get; }
    RollDiceUIPanel RollDicePanel { get; }
    CreatePlayerUIPanel CreatePlayerPanel { get; }
    GameModesUIPanel GameModesPanel { get; }
    InGameUIPanel InGamePanel { get; }
    SelectPlayerTurnUIPanel SelectPlayerTurnPanel { get; }
    LevelSelectUIPanel LevelSelectPanel { get; }
    LevelCompleteUIPanel LevelCompletePanel { get; }
    GameOverUIPanel GameOverPanel { get; }
    SoloGameStageCompleteUIPanel SoloGameStageCompletePanel { get; }
    ItemsUIPopup ItemsPopup { get; }
    StoreUIPopup StorePopup { get; }
    SelectIconUIPanel SelectIconPanel { get; }
}