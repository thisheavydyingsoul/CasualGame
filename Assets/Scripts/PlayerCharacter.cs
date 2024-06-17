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
        }

        public void Accelerate() => _characterMovementController.Accelerate();
        public void Decelerate() => _characterMovementController.Decelerate();

    }
}