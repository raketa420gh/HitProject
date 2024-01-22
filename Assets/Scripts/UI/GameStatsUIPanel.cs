using TMPro;
using UnityEngine;

public class GameStatsUIPanel : UIPanel
{
    [SerializeField] private TMP_Text _scoreCountText;
    [SerializeField] private TMP_Text _timeCountText;
    [SerializeField] private TMP_Text _coinCountText;

    public void SetScore(int scoreCount)
    {
        _scoreCountText.text = scoreCount.ToString();
    }

    public void SetTime(int timeInSeconds)
    {
        _timeCountText.text = timeInSeconds.ToString();
    }

    public void SetCoins(int coinsCount)
    {
        _coinCountText.text = coinsCount.ToString();
    }
}