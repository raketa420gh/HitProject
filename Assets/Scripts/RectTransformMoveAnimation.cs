using System;
using DG.Tweening;
using UnityEngine;

public class RectTransformMoveAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector3 _startAnchorPosition;
    [SerializeField] private Vector3 _endAnchorPosition;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease = Ease.Linear;
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
        _rectTransform.DOAnchorPos3D(_endAnchorPosition, _duration).SetEase(_ease);
    }

    private void ResetToDefault()
    {
        _rectTransform.anchoredPosition3D = _startAnchorPosition;
    }
}
