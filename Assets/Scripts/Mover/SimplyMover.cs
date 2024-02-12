using UnityEngine;

public class SimplyMover : MonoBehaviour, IMover
{
    private float _speed;
    private Vector3 _direction;
    private Vector3 _startPosition;
    private bool _isEnabled;

    private void Awake()
    {
        Disable();
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!_isEnabled)
            return;
        
        transform.Translate(_direction * _speed);
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void StopMove()
    {
        _speed = 0;
        _direction = Vector3.zero;
    }

    public void Reset()
    {
        StopMove();
        transform.position = _startPosition;
    }
}