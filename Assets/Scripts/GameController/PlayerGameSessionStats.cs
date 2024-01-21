using System;

public class PlayerGameSessionStats
{
    private int _categoryPoints;
    private int _trueAnswersCount;

    public int CategoryPoints => _categoryPoints;
    public int TrueAnswersCount => _trueAnswersCount;

    public event Action<PlayerGameSessionStats> OnUpdated;

    public void ResetAll()
    {
        _categoryPoints = 0;
        _trueAnswersCount = 0;
        
        OnUpdated?.Invoke(this);
    }

    public void ResetTrueAnswers()
    {
        _trueAnswersCount = 0;
        
        OnUpdated?.Invoke(this);
    }

    public void AddCategoryPoint()
    {
        _categoryPoints++;
        
        OnUpdated?.Invoke(this);
    }

    public void AddTrueAnswer()
    {
        _trueAnswersCount++;
        
        OnUpdated?.Invoke(this);
    }
}