using System;
using UnityEngine;
using UnityEngine.UI;

public class GameModesUIPanel : UIPanel
{
    [SerializeField] private Button _soloGameButton;
    [SerializeField] private Button _versusGameButton;
    [SerializeField] private Button _timeChallengeGameButton;

    public event Action OnSoloButtonClicked;
    public event Action OnVersusButtonClicked;
    public event Action OnTimeChallengeButtonClicked;

    private void OnEnable()
    {
        _soloGameButton.onClick.AddListener(HandleSoloButtonClick);
        _versusGameButton.onClick.AddListener(HandleVersusButtonClick);
        _timeChallengeGameButton.onClick.AddListener(HandleTimeChallengeButtonClick);
    }

    private void OnDisable()
    {
        _soloGameButton.onClick.RemoveListener(HandleSoloButtonClick);
        _versusGameButton.onClick.RemoveListener(HandleVersusButtonClick);
        _timeChallengeGameButton.onClick.RemoveListener(HandleTimeChallengeButtonClick);
    }

    private void HandleSoloButtonClick()
    {
        OnSoloButtonClicked?.Invoke();
    }

    private void HandleVersusButtonClick()
    {
        OnVersusButtonClicked?.Invoke();
    }

    private void HandleTimeChallengeButtonClick()
    {
        OnTimeChallengeButtonClicked?.Invoke();
    }
}