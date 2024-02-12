public class GameLoopStateMachine : StateMachineController<GameController, GameLoopStateMachine.State>
{
    protected override void RegisterStates()
    {
        RegisterState(new InitializeState(this), State.Initialize);
        RegisterState(new MainMenuState(this), State.MainMenu);
        RegisterState(new LevelCompleteState(this), State.LevelComplete);
        RegisterState(new GameOverState(this), State.GameOver);
        RegisterState(new TestRunState(this), State.TestRun);
    }

    public enum State
    {
        Initialize = 0,
        MainMenu = 2,
        RollDice = 3,
        VersusGame = 5,
        TimeChallenge = 6,
        LevelSelect = 7,
        LevelComplete = 8,
        GameOver = 9,
        TestRun = 10
    }
}