using UnityEngine;

public class UIPanelHud : UIPanel
{
    [SerializeField] private UIIntValueView _gameTimerView;
    [SerializeField] private UIIntValueView _projectilesAmountView;
    
    public UIIntValueView GameTimerView => _gameTimerView;
    public UIIntValueView ProjectilesAmountView => _projectilesAmountView;
}