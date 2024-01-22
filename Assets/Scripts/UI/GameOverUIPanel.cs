using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIPanel : UIPanel
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private GameStatsUIPanel _gameStatsPanel;
    
    public GameStatsUIPanel GameStatsPanel => _gameStatsPanel;
    
    public event Action OnHomeButtonClicked;
    
    private void OnEnable()
    {
        _homeButton.onClick.AddListener(HandleHomeButtonClickEvent);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(HandleHomeButtonClickEvent);
    }
    
    private void HandleHomeButtonClickEvent()
    {
        OnHomeButtonClicked?.Invoke();
    }
}