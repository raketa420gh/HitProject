using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float _startMoveSpeed = 1f;
    [SerializeField] private int _startProjectilesAmount = 10;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    private IFactory _factory;
    private IUIController _uiController;
    private bool _isShootingEnabled;

    private void Update()
    {
        if (!_isShootingEnabled)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _playerBehaviour.ProjectileLauncher.LaunchForwardNewProjectile(Input.mousePosition);
        }
    }

    public void Initialize(IFactory factory, IUIController uiController)
    {
        _factory = factory;
        _uiController = uiController;
        
        _playerBehaviour.Movement.SetSpeed(_startMoveSpeed);
        _playerBehaviour.Movement.SetDirection(Vector3.forward);
        _playerBehaviour.ProjectileLauncher
            .Initialize(_factory, _uiController.HudPanel.ProjectilesAmountView, _startProjectilesAmount);
        
        DisableShooting();
    }

    public void StartMoveForward()
    {
        _playerBehaviour.Movement.Enable();
    }

    public void StopMove()
    {
        _playerBehaviour.Movement.StopMove();
    }

    public void EnableShooting()
    {
        _isShootingEnabled = true;
    }

    public void DisableShooting()
    {
        _isShootingEnabled = false;
    }
}