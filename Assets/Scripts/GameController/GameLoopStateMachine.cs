public class GameLoopStateMachine : StateMachineController<GameController, GameLoopStateMachine.State>
{
    protected override void RegisterStates()
    {
        RegisterState(new InitializeState(this), State.Initialize);
        RegisterState(new MainMenuState(this), State.MainMenu);
    }

    public enum State
    {
        Initialize = 0,
        MainMenu = 1
    }
}