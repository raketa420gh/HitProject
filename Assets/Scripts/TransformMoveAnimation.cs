using DG.Tweening;
using UnityEngine;

public class TransformMoveAnimation : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease = Ease.Linear;
    [SerializeField] private bool _isLooped = true;
    [SerializeField] private bool _activateEnabled = false;

    private void OnEnable()
    {
        if (!_activateEnabled)
            return;
        
        ActivateAnimation();
    }

    public void ActivateAnimation()
    {
        ResetToDefault();
        
        if (_isLooped)
            _transform.DOMove(_endPosition, _duration).SetEase(_ease).SetLoops(-1, LoopType.Yoyo);
        else
            _transform.DOMove(_endPosition, _duration).SetEase(_ease).SetLoops(0);
    }

    private void ResetToDefault()
    {
        _transform.position = _startPosition;
    }
}
