using UnityEngine;

public class UIPanel : MonoBehaviour, IUIPanel
{
    protected bool _isActive;
    
    public bool IsActive => _isActive;

    public virtual void Show()
    {
        if (_isActive)
            return;
        
        gameObject.SetActive(true);
        _isActive = true;
    }

    public virtual void Hide()
    {
        if (!_isActive)
            return;
        
        gameObject.SetActive(false);
        _isActive = false;
    }
}