using CasualGame.FSM;
using UnityEngine;

namespace CasualGame.Enemy.States
{
    public class RunAwayState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private float _runAwaySpeedDiff;

        public RunAwayState(EnemyTarget target, EnemyDirectionController enemyDirectionController, float runAwaySpeedDiff)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _runAwaySpeedDiff = runAwaySpeedDiff;
        }
        public override void Execute()
        {
            Vector3 targetPosition = _target.PlayerCoordinates();
            _enemyDirectionController.UpdateMovementDirection(new Vector3(- targetPosition.x, 0, -targetPosition.z));
        }
        public override void StartExecuting()
        {
            _enemyDirectionController.StartRunAway(_runAwaySpeedDiff);
        }
        public override void StopExecuting()
        {
            _enemyDirectionController.StopRunAway(_runAwaySpeedDiff);
        }
    }
}
