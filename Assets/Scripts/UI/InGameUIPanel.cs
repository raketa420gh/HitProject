using UnityEngine;

public class InGameUIPanel : UIPanel
{
    [SerializeField] private QuestionUIPanel _questionPanel;
    [SerializeField] private ProgressBarUIPanel _turnTimeProgressBar;
    [SerializeField] private ProgressBarUIPanel _globalTimeProgressBar;

    public QuestionUIPanel QuestionPanel => _questionPanel;
    public ProgressBarUIPanel TurnTimeProgressBar => _turnTimeProgressBar;
    public ProgressBarUIPanel GlobalTimeProgressBar => _globalTimeProgressBar;
}