using CasualGame.Movement;
using CasualGame.Shooting;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CasualGame
{

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {

        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private float _health = 2f;
        protected IMovementDirectionSource _movementDirectionSource;

        private Dictionary<Type, float> _boosts = new Dictionary<Type, float>();

        protected  CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;



        protected virtual void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected virtual void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }
        protected virtual void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            if (_health <= 0)
                Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter(Collider other)
        { 
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                _health -= bullet.Damage;
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
    }
}
