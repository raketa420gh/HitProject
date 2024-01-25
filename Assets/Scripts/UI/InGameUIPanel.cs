using UnityEngine;

public class InGameUIPanel : UIPanel
{
    [SerializeField] private QuestionUIPanel _questionPanel;
    [SerializeField] private ProgressBarUIPanel _turnTimeProgressBar;
    [SerializeField] private ProgressBarUIPanel _globalTimeProgressBar;
    [Header("Deactivate main background")] 
    [SerializeField] private GameObject _mainBackground;

    public QuestionUIPanel QuestionPanel => _questionPanel;
    public ProgressBarUIPanel TurnTimeProgressBar => _turnTimeProgressBar;
    public ProgressBarUIPanel GlobalTimeProgressBar => _globalTimeProgressBar;

    private void OnEnable()
    {
        _mainBackground.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _mainBackground.gameObject.SetActive(true);
    }
}