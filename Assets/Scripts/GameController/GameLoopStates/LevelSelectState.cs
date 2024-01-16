using UnityEngine;

public class LevelSelectState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly LevelSelectUIPanel _levelSelectPanel;
    private readonly ILevelController _levelController;
    
    public LevelSelectState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _levelSelectPanel = gameLoopStateMachine.Parent.UIController.LevelSelectPanel;
        _levelController = gameLoopStateMachine.Parent.LevelController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");
        
        _levelController.OnLevelSelected += HandleLevelSelectEvent;
        _levelController.InitializeSelectLevelPanel();
        _levelSelectPanel.Show();
    }

    public override void OnStateDisabled()
    {
        _levelController.OnLevelSelected -= HandleLevelSelectEvent;
        
        _levelSelectPanel.Hide();
    }

    public override void Update()
    {
        
    }
    
    private void HandleLevelSelectEvent(int levelNumber)
    {
        _levelController.SetGameMode(GameModeType.Solo);

        SoloGameState soloGameState = (SoloGameState)_gameLoopStateMachine.GetState(GameLoopStateMachine.State.SoloGame);
        soloGameState.SetLevel(levelNumber);
        
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
    }
}