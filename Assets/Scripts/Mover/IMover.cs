using UnityEngine;

public interface IMover
{
    void Enable();
    void Disable();
    void SetSpeed(float speed);
    void SetDirection(Vector3 direction);
    void StopMove();
    void Reset();
}