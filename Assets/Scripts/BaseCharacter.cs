using CasualGame.Movement;
using CasualGame.PickUp;
using CasualGame.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CasualGame
{

    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        private readonly Dictionary<BonusPickUp, float> _bonusesAndTimersSeconds = new Dictionary<BonusPickUp, float>();

        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        protected float _health = 2f;

        protected float _maxHealth = 2f;

        protected IMovementDirectionSource _movementDirectionSource;

        protected CharacterMovementController _characterMovementController;
        protected ShootingController _shootingController;



        protected virtual void Awake()
        {
            _maxHealth = _health;
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected virtual void Start()
        {
            _shootingController.SetWeapon(_baseWeaponPrefab, _hand);
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
            BonusPickUp[] bonuses = _bonusesAndTimersSeconds.Keys.ToArray();
            foreach (BonusPickUp bonus in bonuses)
            {
                if (_bonusesAndTimersSeconds[bonus] <= 0)
                {
                    bonus.EndBonus(this);
                    _bonusesAndTimersSeconds.Remove(bonus);
                    continue;
                }
                _bonusesAndTimersSeconds[bonus] -= Time.deltaTime;
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        { 
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                _health -= bullet.Damage;
                Destroy(other.gameObject);
            }
            if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<ItemPickUp>();
                pickUp.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        public void Accelerate(float acceleration) => _characterMovementController.Accelerate(acceleration);
        public void Decelerate(float deceleration) => _characterMovementController.Decelerate(deceleration);
        public void PickUpBonus(BonusPickUp bonus, float timeSeconds)
        {
            if (_bonusesAndTimersSeconds.TryAdd(bonus, timeSeconds))
                return;
            _bonusesAndTimersSeconds[bonus] = timeSeconds;
        }
        public virtual void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
    }
}
