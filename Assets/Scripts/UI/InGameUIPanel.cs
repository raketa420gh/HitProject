using UnityEngine;

public class InGameUIPanel : UIPanel
{
    [SerializeField] private QuestionUIPanel _questionPanel;

    private QuestionUIPanel QuestionPanel => _questionPanel;
}