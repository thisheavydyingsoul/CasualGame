using UnityEngine;

namespace CasualGame.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private float _acceleration = 1f;

        [SerializeField]
        private float _maxRadiansDelta = 10f;
        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private float _accelerationTimerSeconds = 0f;
        private float _bonusAcceleration = 1f;
        private bool _speedIncreased = false;

        private CharacterController _characterController;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        { 
            if (_speedIncreased && _accelerationTimerSeconds <= 0)
            {
                _speed /= _bonusAcceleration;
                _speedIncreased = false;
                _bonusAcceleration = 1f;
            }
            else if(_speedIncreased)
                _accelerationTimerSeconds -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
                _speed *= _acceleration;
            if (Input.GetKeyUp(KeyCode.Space))
                _speed /= _acceleration;
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = MovementDirection * _speed * Time.deltaTime;
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - MovementDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }
        public void IncreaseSpeed(float acceleration, float timeSeconds)
        {
            _bonusAcceleration = acceleration;
            _accelerationTimerSeconds = timeSeconds;
            _speed *= acceleration;
            _speedIncreased = true;
        }
    }
}