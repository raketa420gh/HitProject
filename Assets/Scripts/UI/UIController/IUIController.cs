public interface IUIController
{
    HudUIPanel HudPanel { get; }
    MainMenuUIPanel MainMenuPanel { get; }
    LevelCompleteUIPanel LevelCompletePanel { get; }
    GameOverUIPanel GameOverPanel { get; }
}