using TMPro;
using UnityEngine;

public class PlayersInfoUIPanel : UIPanel
{
    [SerializeField] private PlayerInfoUIPanel _youPlayerPanel;
    [SerializeField] private PlayerInfoUIPanel _oppenentPlayerPanel;
    [SerializeField] private TMP_Text _infoText;

    public PlayerInfoUIPanel YouPlayerPanel => _youPlayerPanel;
    public PlayerInfoUIPanel OpponentPlayerPanel => _oppenentPlayerPanel;

    public void SetInfoText(string text)
    {
        _infoText.text = text;
    }

    public void SetInfoTextColor(Color color)
    {
        _infoText.color = color;
    }

    public void EnableInfoText()
    {
        _infoText.gameObject.SetActive(true);
    }

    public void DisableInfoText()
    {
        _infoText.gameObject.SetActive(false);
    }
}