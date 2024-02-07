using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DicePhysical : MonoBehaviour
{
    [Header("Physics")] 
    [SerializeField] private Vector3 _impulseDirection = Vector3.back;
    [SerializeField] private float _forceUpImpulse = 3000;
    [SerializeField] private float _randomTorqueAmplitudeRight = 300f;
    [SerializeField] private float _randomTorqueAmplitudeDown = 300f;
    [Header("View")]
    [SerializeField] private DiceSide[] _diceSides = new DiceSide[6];
    [SerializeField] private AudioSource _audioSource;
    private Rigidbody _diceRigidbody;
    private bool _isDiceStopCheckActive;
    private Vector3 _startDicePosition;
    private Quaternion _startDiceRotation;
    
    public event Action<QuestionCategoryType> OnRollDiceCompleted;

    private void Awake()
    {
        _diceRigidbody = GetComponent<Rigidbody>();
        _diceRigidbody.solverIterations = 250;
        _startDicePosition = transform.position;
        _startDiceRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        UpdateDicePhysics();
    }

    public void Reroll()
    {
        ResetDice();
        
        float randomTorqueRight = Random.Range(-_randomTorqueAmplitudeRight, _randomTorqueAmplitudeRight);
        float randomTorqueDown = Random.Range(-_randomTorqueAmplitudeDown, _randomTorqueAmplitudeDown);
        
        _diceRigidbody.AddForce(_impulseDirection * _forceUpImpulse);
        
        _diceRigidbody.AddForce(Vector3.right * randomTorqueRight / 2);
        _diceRigidbody.AddForce(Vector3.down * randomTorqueDown / 2);
        
        _diceRigidbody.AddTorque(Vector3.right * randomTorqueRight);
        _diceRigidbody.AddTorque(Vector3.down * randomTorqueDown);

        _isDiceStopCheckActive = true;
    }

    private void UpdateDicePhysics()
    {
        if (_isDiceStopCheckActive)
        {
            if (_diceRigidbody.IsSleeping())
            {
                _isDiceStopCheckActive = false;

                Debug.Log("Dice stopped");
                
                Debug.Log($"Dice rotation euler angles = {_diceRigidbody.rotation.eulerAngles}");

                DiceSide resultSide = _diceSides[0];
                float minZ = resultSide.transform.position.z;

                for (int i = 1; i < _diceSides.Length; i++)
                {
                    float currentZ = _diceSides[i].transform.position.z;

                    if (currentZ < minZ)
                    {
                        minZ = currentZ;
                        resultSide = _diceSides[i];
                    }
                }

                Debug.Log("Result side (back): " + resultSide.QuestionCategoryType);

                OnRollDiceCompleted?.Invoke(resultSide.QuestionCategoryType);
            }
        }
    }

    private void ResetDice()
    {
        _diceRigidbody.position = _startDicePosition;
        _diceRigidbody.rotation = _startDiceRotation;
    }
}