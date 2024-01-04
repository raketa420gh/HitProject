using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;

    public Camera MainCamera => _mainCamera;
    
    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }
}