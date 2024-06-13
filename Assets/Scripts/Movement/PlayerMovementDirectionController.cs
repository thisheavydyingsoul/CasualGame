using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CasualGame.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;

        public event Action OnAcceleration;
        public event Action OnDeceleration;

        public Vector3 MovementDirection { get; private set; }
        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        protected void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space)) OnAcceleration?.Invoke();
            else if (Input.GetKeyUp(KeyCode.Space)) OnDeceleration?.Invoke();
            var direction = new Vector3(horizontal, 0, vertical);
            direction = _camera.transform.rotation * direction;
            direction.y = 0;

            MovementDirection = direction.normalized;
        }

    }
}