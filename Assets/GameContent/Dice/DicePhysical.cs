using UnityEngine;
using Random = UnityEngine.Random;

public class DicePhysical : MonoBehaviour
{
    [SerializeField] private float _forceUpImpulse = 3000;
    [SerializeField] private float _randomTorqueAmplitude = 300f;
    [SerializeField] private DiceSide[] _diceSides = new DiceSide[6];
    [SerializeField] private AudioSource _audioSource;
    private Rigidbody _diceRigidbody;
    private bool _isDiceStopCheckActive;
    
    private void Awake()
    {
        _diceRigidbody = GetComponent<Rigidbody>();
        _diceRigidbody.solverIterations = 250;
    }

    private void Start()
    {
        float randomTorque = Random.Range(-_randomTorqueAmplitude, _randomTorqueAmplitude);
        _diceRigidbody.AddForce(Vector3.up * _forceUpImpulse);
        _diceRigidbody.AddTorque(Vector3.right * randomTorque);

        _isDiceStopCheckActive = true;
    }

    private void FixedUpdate()
    {
        if (_isDiceStopCheckActive)
        {
            if (_diceRigidbody.IsSleeping())
            {
                _isDiceStopCheckActive = false;
                
                Debug.Log("Dice stopped");
                
                DiceSide diceSideWithMaxY = _diceSides[0];
                float maxY = diceSideWithMaxY.transform.position.y;

                for (int i = 1; i < _diceSides.Length; i++)
                {
                    float currentY = _diceSides[i].transform.position.y;
                    
                    if (currentY > maxY)
                    {
                        maxY = currentY;
                        diceSideWithMaxY = _diceSides[i];
                    }
                }
                
                Debug.Log("Верхняя грань: " + diceSideWithMaxY.QuestionCategoryType);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _audioSource.Play();
    }
}