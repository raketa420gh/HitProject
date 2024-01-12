using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUIPanel : UIPanel
{
    [SerializeField] private StarsUIPanel _starsPanel;
    [SerializeField] private UIPanel _gameStatsPanel;
    [SerializeField] private TMP_Text _scoreCountText;
    [SerializeField] private TMP_Text _timeCountText;
    [SerializeField] private TMP_Text _coinCountText;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _nextLevelButton;

    public event Action OnHomeButtonClicked;
    public event Action OnReplayButtonClicked;
    public event Action OnNextLevelButtonClicked;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(HandleHomeButtonClickEvent);
        _replayButton.onClick.AddListener(HandleReplayButtonClickEvent);
        _nextLevelButton.onClick.AddListener(HandleNextLevelButtonClickEvent);
        _starsPanel.OnStarsAnimationCompleted += HandleStarsAnimationCompleteEvent;
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(HandleHomeButtonClickEvent);
        _replayButton.onClick.RemoveListener(HandleReplayButtonClickEvent);
        _nextLevelButton.onClick.RemoveListener(HandleNextLevelButtonClickEvent);
        _starsPanel.OnStarsAnimationCompleted -= HandleStarsAnimationCompleteEvent;
    }

    public override void Show()
    {
        base.Show();
        
        _starsPanel.ShowStarsAnimation();
        _homeButton.interactable = false;
        _gameStatsPanel.Hide();
    }

    public void SetGameStats(int scoreCount, int timeSeconds, int coinsCount)
    {
        _scoreCountText.text = scoreCount.ToString();
        _timeCountText.text = timeSeconds.ToString();
        _coinCountText.text = coinsCount.ToString();
    }

    private void HandleHomeButtonClickEvent()
    {
        OnHomeButtonClicked?.Invoke();
    }

    private void HandleReplayButtonClickEvent()
    {
        OnReplayButtonClicked?.Invoke();
    }

    private void HandleNextLevelButtonClickEvent()
    {
        OnNextLevelButtonClicked?.Invoke();
    }

    private void HandleStarsAnimationCompleteEvent()
    {
        _gameStatsPanel.Show();
        _homeButton.interactable = true;
    }
}