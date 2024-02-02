using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUIPanel : UIPanel
{
    [SerializeField] private Image _playerIcon;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private Button _openIconSelectButton;
    
    private int _selectedIconNumber;

    public string PlayerName => _playerNameText.text;
    public int IconNumber => _selectedIconNumber;

    public Button OpenIconSelectButton => _openIconSelectButton;

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

    public void SetPlayerName(string playerName)
    {
        _playerNameText.text = playerName;
    }

    public void SetIcon(Sprite sprite)
    {
        _playerIcon.sprite = sprite;
    }

    public void SetIconNumber(int number)
    {
        _selectedIconNumber = number;
    }
}