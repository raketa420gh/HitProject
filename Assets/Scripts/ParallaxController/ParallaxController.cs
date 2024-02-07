using DG.Tweening;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] _layersTransforms;
    [SerializeField] private float _parallaxEffectMultiplier;
    [SerializeField] private Transform _anchorTransform;

    private Vector2 _previousAnchorPosition;
    private Vector3 _anchorStartPosition;
    private Vector3[] _layersStartPositions;
    private bool _isEnabled;

    public void Initialize()
    {
        _anchorStartPosition = _anchorTransform.position;
        _previousAnchorPosition = transform.position;

        _layersStartPositions = new Vector3[_layersTransforms.Length];
        
        for (int i = 0; i < _layersTransforms.Length; i++)
            _layersStartPositions[i] = _layersTransforms[i].position;
    }

    public void UpdateParallax()
    {
        if (!_isEnabled)
            return;
        
        Vector2 anchorPosition = (Vector2)_anchorTransform.position;
        Vector2 anchorPositionDelta = anchorPosition - _previousAnchorPosition;

        for (int i = 0; i < _layersTransforms.Length; i++)
        {
            float parallaxEffect = (i + 1) * _parallaxEffectMultiplier;

            Vector3 backgroundTargetPos = new Vector3(_layersTransforms[i].position.x + anchorPositionDelta.x * parallaxEffect, 
                _layersTransforms[i].position.y + anchorPositionDelta.y * parallaxEffect, _layersTransforms[i].position.z);
            
            _layersTransforms[i].position = Vector3.Lerp(_layersTransforms[i].position, backgroundTargetPos, Time.deltaTime);
        }

        _previousAnchorPosition = anchorPosition;
    }

    public void EnableParallax()
    {
        _isEnabled = true;
    }

    public void DisableParallax()
    {
        _isEnabled = false;
    }

    public void DoParallaxHorizontalStep(float xDistance, float seconds)
    {
        Vector3 newPosition = _anchorTransform.position + Vector3.left * xDistance;

        _anchorTransform.DOMove(newPosition, seconds);
    }

    public void ResetPositions()
    {
        for (int i = 0; i < _layersTransforms.Length; i++)
            _layersTransforms[i].position = _layersStartPositions[i];

        _anchorTransform.position = _anchorStartPosition;
    }
}