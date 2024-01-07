public class GameLoopStateMachine : StateMachineController<GameController, GameLoopStateMachine.State>
{
    protected override void RegisterStates()
    {
        RegisterState(new InitializeState(this), State.Initialize);
        RegisterState(new CreatePlayerState(this), State.CreatePlayer);
        RegisterState(new MainMenuState(this), State.MainMenu);
        RegisterState(new RollDiceState(this), State.RollDice);
    }

    public enum State
    {
        Initialize = 0,
        CreatePlayer = 1,
        MainMenu = 2,
        RollDice = 3
    }
}