using CasualGame.FSM;
using UnityEngine;

namespace CasualGame.Enemy.States
{
    public class IdleState : BaseState
    {
        private readonly EnemyDirectionController _directionController;
        private readonly EnemyTarget _target;
        public IdleState(EnemyDirectionController directionController, EnemyTarget target)
        {
            _directionController = directionController;
            _target = target;
        }
        public override void Execute()
        {
         
        }
        public override void StartExecuting()
        {
            _directionController.UpdateMovementDirection(_target.AgentTransform());
        }
        public override void StopExecuting()
        {

        }
    }
}
