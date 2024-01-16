public class GameLoopStateMachine : StateMachineController<GameController, GameLoopStateMachine.State>
{
    protected override void RegisterStates()
    {
        RegisterState(new InitializeState(this), State.Initialize);
        RegisterState(new CreatePlayerState(this), State.CreatePlayer);
        RegisterState(new MainMenuState(this), State.MainMenu);
        RegisterState(new RollDiceState(this), State.RollDice);
        RegisterState(new SoloGameState(this), State.SoloGame);
        RegisterState(new LevelCompleteState(this), State.LevelComplete);
        RegisterState(new GameOverState(this), State.GameOver);
        RegisterState(new LevelSelectState(this), State.LevelSelect);
    }

    public enum State
    {
        Initialize = 0,
        CreatePlayer = 1,
        MainMenu = 2,
        RollDice = 3,
        SoloGame = 4,
        LevelComplete = 5,
        GameOver = 6,
        LevelSelect = 7
    }
}