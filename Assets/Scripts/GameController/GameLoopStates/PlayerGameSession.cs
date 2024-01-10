using UnityEngine;

public class PlayerGameSession
{
    private int _categoryPoints;
    private int _trueAnswersCount;

    public int CategoryPoints => _categoryPoints;
    public int TrueAnswersCount => _trueAnswersCount;

    public void ResetAll()
    {
        _categoryPoints = 0;
        _trueAnswersCount = 0;
    }

    public void ResetTrueAnswers()
    {
        _trueAnswersCount = 0;
    }

    public void AddCategoryPoint()
    {
        _categoryPoints++;
    }

    public void AddTrueAnswer()
    {
        _trueAnswersCount++;
    }
}