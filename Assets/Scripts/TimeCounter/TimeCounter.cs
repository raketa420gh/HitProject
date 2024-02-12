using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private UIIntValueView _view;
    private float _timer;
    private bool _isActive = false;

    private void Update()
    {
        if (!_view)
            return;
        
        if (!_isActive)
            return;

        _timer += Time.deltaTime;
        
        _view.SetValueView((int)_timer);
    }

    public void Initialize(UIIntValueView timerView)
    {
        _view = timerView;
    }

    public void StartTimeCounting()
    {
        Reset();
        
        _isActive = true;
    }

    public void StopTimeCounting()
    {
        _isActive = false;
    }
    
    private void Reset()
    {
        _timer = 0f;
    }
}