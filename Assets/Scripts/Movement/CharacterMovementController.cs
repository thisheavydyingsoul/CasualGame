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
        private float _acceleratedSpeed = 1f;

        [SerializeField]
        private float _maxRadiansDelta = 10f;

        private float _actualSpeed = 1f;
        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;

        private bool _isIdle = false;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _actualSpeed = _speed;
        }

        protected void Update()
        {
            if (_isIdle)
                return;
            Translate();
            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }

        private void Translate()
        {
            var delta = MovementDirection * _actualSpeed * Time.deltaTime;
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

        public void Accelerate() => _actualSpeed = _acceleratedSpeed;

        public void Decelerate() => _actualSpeed = _speed;

        public void Accelerate(float acceleration) { 
            _actualSpeed *= acceleration;
            _speed *= acceleration;
            _acceleratedSpeed *= acceleration;
        }
        public void Decelerate(float deceleration)
        {
            _actualSpeed /= deceleration;
            _speed /= deceleration;
            _acceleratedSpeed /= deceleration;
        }

        public void StartRunAway(float runAwaySpeedDiff) => _actualSpeed += runAwaySpeedDiff;

        public void StopRunAway(float runAwaySpeedDiff) => _actualSpeed -= runAwaySpeedDiff;

        public void StopMoving() => _isIdle = true;

    }
}