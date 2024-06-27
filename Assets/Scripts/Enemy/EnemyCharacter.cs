using Casualgame.Enemy.States;
using CasualGame.Scripts;
using CasualGame.Shooting;
using UnityEngine;

namespace CasualGame.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacter : BaseCharacter
    {
        private EnemyAiController _aiController;
        private EnemyDirectionController _directionController;
        protected override void Awake()
        {
            base.Awake();
            _aiController = GetComponent<EnemyAiController>();
            _directionController = GetComponent<EnemyDirectionController>();
            _directionController.OnStartRunAway += StartRunAway;
            _directionController.OnStopRunAway += StopRunAway;

        }

        protected override void Update()
        {
            base.Update();
            if (_health <= 0)
                return;
            if (_health / _maxHealth * 100 < _aiController.HPToRunAwayPercent)
                _aiController.HPToRunAway();
        }

        public override void SetWeapon(Weapon weapon)
        {
            base.SetWeapon(weapon);
            _aiController.WeaponChanged();
        }

        public void StartRunAway(float _runAwaySpeedDiff)
        {
            _characterMovementController.StartRunAway(_runAwaySpeedDiff);
            _shootingController.RunAway = true;
        }

        public void StopRunAway(float _runAwaySpeedDiff)
        {
            _characterMovementController.StopRunAway(_runAwaySpeedDiff);
            _shootingController.RunAway = false;
        }

        public void Death() {
            _levelOverseer.EnemyKilled();
            Destroy(gameObject);
        }
    }
}