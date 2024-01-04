using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/QuestionData")]
public class QuestionData : ScriptableObject
{
    [SerializeField] private string _question;
    [SerializeField] private string[] _answers;
    [SerializeField] private int _correctAnswerIndex;
    [SerializeField] private QuestionCategoryType _categoryType;
    [SerializeField] private DifficultyType _difficultyType;

    public string Question => _question;
    public string[] Answers => _answers;
    public int CorrectAnswerIndex => _correctAnswerIndex;
    public QuestionCategoryType CategoryType => _categoryType;
    public DifficultyType DifficultyType => _difficultyType;
}