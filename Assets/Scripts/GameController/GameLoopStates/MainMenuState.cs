using UnityEngine;

public class MainMenuState : GameLoopState
{
    private readonly GameLoopStateMachine _gameLoopStateMachine;
    private readonly IUIController _uiController;
    private readonly GameModesUIPanel _gameModesPanel;
    private readonly ILevelController _levelController;

    public MainMenuState(GameLoopStateMachine gameLoopStateMachine) : base(gameLoopStateMachine)
    {
        _gameLoopStateMachine = gameLoopStateMachine;
        _uiController = _gameLoopStateMachine.Parent.UIController;
        _gameModesPanel = _uiController.GameModesPanel;
        _levelController = _gameLoopStateMachine.Parent.LevelController;
    }

    public override void OnStateRegistered()
    {
        Debug.Log($"{this} registered");
    }

    public override void OnStateActivated()
    {
        Debug.Log($"{this} entered");

        _uiController.GameModesPanel.OnSoloButtonClicked += HandleSoloGameStartButtonEvent;
        _uiController.GameModesPanel.OnVersusButtonClicked += HandleVersusGameStartButtonEvent;
        _uiController.GameModesPanel.OnTimeChallengeButtonClicked += HandleTimeChallengeGameStartButtonEvent;

        InitializeMainMenu();
        InitializePlayerInfoPanelsView();
    }

    public override void OnStateDisabled()
    {
        _uiController.GameModesPanel.OnSoloButtonClicked += HandleSoloGameStartButtonEvent;
        _uiController.GameModesPanel.OnVersusButtonClicked += HandleVersusGameStartButtonEvent;
        _uiController.GameModesPanel.OnTimeChallengeButtonClicked += HandleTimeChallengeGameStartButtonEvent;

        _gameModesPanel.Hide();
    }

    public override void Update()
    {
    }

    private void InitializePlayerInfoPanelsView()
    {
        _uiController.PlayersInfoPanel.OpponentPlayerPanel.SetView(false);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetScoreView(false);
        _uiController.PlayersInfoPanel.YouPlayerPanel.SetView(true);
        _uiController.PlayersInfoPanel.gameObject.SetActive(true);
        _uiController.PlayersInfoPanel.SetInfoText("");
    }

    private void InitializeMainMenu()
    {
        _uiController.MainMenuPanel.SetStartButtonObjectView(true);
    }

    private void HandleSoloGameStartButtonEvent()
    {
        Debug.Log($"START SOLO GAME");

        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.LevelSelect);
    }

    private void HandleVersusGameStartButtonEvent()
    {
        Debug.Log($"START VERSUS GAME");
        
        _levelController.SetGameMode(GameModeType.Versus);
        _gameLoopStateMachine.SetState(GameLoopStateMachine.State.RollDice);
    }

    private void HandleTimeChallengeGameStartButtonEvent()
    {
        Debug.Log($"START TIME CHALLENGE GAME");

        _levelController.SetGameMode(GameModeType.TimeChallenge);
    }
}