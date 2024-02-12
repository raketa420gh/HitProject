using System;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private Fracture _fracture;
    public event Action OnDestroyed;

    private void OnEnable()
    {
        _fracture.callbackOptions.onCompleted.AddListener(HandleDestroyEvent);
    }

    private void OnDisable()
    {
        _fracture.callbackOptions.onCompleted.RemoveListener(HandleDestroyEvent);
    }
    
    private void HandleDestroyEvent()
    {
        OnDestroyed?.Invoke();
    }
}

public class DestroyableObjectAddProjectiles : DestroyableObject
{
    [SerializeField] private int _projectilesAddAmount = 3;
    
    
}