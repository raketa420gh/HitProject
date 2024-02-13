using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private SimplyMover _movement;
    [SerializeField] private ProjectileLauncher _projectileLauncher;
    [SerializeField] private PlayerCollider _playerCollider;

    public IMover Movement => _movement;
    public IProjectileLauncher ProjectileLauncher => _projectileLauncher;
    public PlayerCollider PlayerCollider => _playerCollider;
}