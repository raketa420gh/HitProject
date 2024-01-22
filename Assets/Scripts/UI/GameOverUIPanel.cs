using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIPanel : UIPanel
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private GameStatsUIPanel _gameStatsPanel;
    
    public GameStatsUIPanel GameStatsPanel => _gameStatsPanel;
    
    public event Action OnHomeButtonClicked;
    public event Action OnReplayButtonClicked;
    
    private void OnEnable()
    {
        _homeButton.onClick.AddListener(HandleHomeButtonClickEvent);
        _replayButton.onClick.AddListener(HandleReplayButtonClickedEvent);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(HandleHomeButtonClickEvent);
        _replayButton.onClick.RemoveListener(HandleReplayButtonClickedEvent);
    }
    
    private void HandleHomeButtonClickEvent()
    {
        OnHomeButtonClicked?.Invoke();
    }

    private void HandleReplayButtonClickedEvent()
    {
        OnReplayButtonClicked?.Invoke();
    }
}