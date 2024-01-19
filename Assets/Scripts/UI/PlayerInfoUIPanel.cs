using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUIPanel : UIPanel
{
    [SerializeField] private Image _playerIcon;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerScoreText;

    public string PlayerName => _playerNameText.text;

    public void SetView(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetScoreView(bool isActive)
    {
        _playerScoreText.gameObject.SetActive(isActive);
    }

    public void SetScore(int scoreCount)
    {
        _playerScoreText.text = scoreCount.ToString();
    }

    public void SetPlayerInfo(string playerName)
    {
        _playerNameText.text = playerName;
    }
}