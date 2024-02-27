using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private SimplyMover _movement;
    [SerializeField] private ProjectileLauncher _projectileLauncher;

    public IMover Movement => _movement;
    public IProjectileLauncher ProjectileLauncher => _projectileLauncher;
}