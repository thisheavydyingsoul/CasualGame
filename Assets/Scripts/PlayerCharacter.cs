using CasualGame.Movement;
using CasualGame.PickUp;
using CasualGame.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CasualGame
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {

        protected override void Awake()
        {
            base.Awake();
            ((PlayerMovementDirectionController)_movementDirectionSource).OnAcceleration += Accelerate;
            ((PlayerMovementDirectionController)_movementDirectionSource).OnDeceleration += Decelerate;
            _levelOverseer.levelCompleted += OnLevelCompleted;
            _levelOverseer.levelCompleted += _characterMovementController.StopMoving;
        }

        private void Accelerate() => _characterMovementController.Accelerate();
        private void Decelerate() => _characterMovementController.Decelerate();

        private void OnLevelCompleted() {
            _shootingController.RemoveWeapon();
            _levelOverseer.levelCompleted -= OnLevelCompleted;
            _levelOverseer.levelCompleted -= _characterMovementController.StopMoving;
            _animator.SetTrigger("LevelCompleted");
        }
    }
}