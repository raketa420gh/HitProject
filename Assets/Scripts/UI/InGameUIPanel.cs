using UnityEngine;

public class InGameUIPanel : UIPanel
{
    [SerializeField] private QuestionUIPanel _questionPanel;

    public QuestionUIPanel QuestionPanel => _questionPanel;
}