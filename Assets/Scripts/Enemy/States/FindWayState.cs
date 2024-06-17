using CasualGame.FSM;
using UnityEngine;

namespace CasualGame.Enemy.States
{
    public class FindWayState : BaseState
    {
        private const float MaxDistanceBetweenRealAndCalculated = 3f;

        private readonly EnemyTarget _target;
        private readonly NavMesher _navMesher;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;

        public FindWayState(EnemyTarget target, NavMesher navMesher, EnemyDirectionController enemyDirectionController)
        {
            _target = target;
            _navMesher = navMesher;
            _enemyDirectionController = enemyDirectionController;
        }
        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;

            if (!_navMesher.PathCalculated || 
                _navMesher.DistanceToTargetPoint(targetPosition) > MaxDistanceBetweenRealAndCalculated)
                _navMesher.CalculatePath(targetPosition);

            var currentPoint = _navMesher.GetCurrentPoint();
            if (_currentPoint != currentPoint)
            {
                _currentPoint = currentPoint;
                _enemyDirectionController.UpdateMovementDirection(currentPoint);
            }
        }
        public override void StartExecuting()
        {
            //Debug.Log("FindWay");
        }
        public override void StopExecuting()
        {
            
        }
    }
}
