using CasualGame.Movement;
using CasualGame.PickUp;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CasualGame
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        private Dictionary<BonusPickUp, float> _bonusesAndTimersSeconds = new Dictionary<BonusPickUp, float>();

        protected override void Awake()
        {
            base.Awake();
            ((PlayerMovementDirectionController)_movementDirectionSource).OnAcceleration += Accelerate;
            ((PlayerMovementDirectionController)_movementDirectionSource).OnDeceleration += Decelerate;;

        }

        protected override void Update()
        {
            base.Update();
            BonusPickUp[] bonuses = _bonusesAndTimersSeconds.Keys.ToArray();
            foreach(BonusPickUp bonus in bonuses)
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
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<ItemPickUp>();
                pickUp.PickUp(this);
                Destroy(other.gameObject);
            }
        }
        public void Accelerate() => _characterMovementController.Accelerate();
        public void Decelerate() => _characterMovementController.Decelerate();
        public void Accelerate(float acceleration) => _characterMovementController.Accelerate(acceleration);
        public void Decelerate(float deceleration) => _characterMovementController.Decelerate(deceleration);
        public void PickUpBonus(BonusPickUp bonus, float timeSeconds)
        {
            if (_bonusesAndTimersSeconds.TryAdd(bonus, timeSeconds))
                return;
            _bonusesAndTimersSeconds[bonus] = timeSeconds;
        }
    }
}