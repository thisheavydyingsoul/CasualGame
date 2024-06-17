
using UnityEngine;
using UnityEngine.AI;

namespace CasualGame.Enemy
{
    public class NavMesher
    {
        private const float DistanceEps = 1.5f;
        public bool PathCalculated { get; private set; }

        private readonly NavMeshQueryFilter _filter;
        private readonly Transform _agentTransform;

        private NavMeshPath _navMeshPath = new NavMeshPath();
        private NavMeshHit _targetHit;
        private int _currentPathPointIndex;
        public NavMesher(Transform agentTransform)
        {
            _filter = new NavMeshQueryFilter { areaMask = NavMesh.AllAreas};
            PathCalculated = false;
            _agentTransform = agentTransform;
        }
        
        public void CalculatePath(Vector3 targetPosition)
        {
            NavMesh.SamplePosition(_agentTransform.position, out var agentHit, 10f, _filter);
            NavMesh.SamplePosition(targetPosition, out _targetHit, 10f, _filter);

            PathCalculated = NavMesh.CalculatePath(agentHit.position, _targetHit.position, _filter, _navMeshPath);
            _currentPathPointIndex = 0;
        }

        public Vector3 GetCurrentPoint()
        {
            var currentPoint = _navMeshPath.corners[_currentPathPointIndex];
            var distance = (_agentTransform.position - currentPoint).magnitude;

            if (distance < DistanceEps)
                _currentPathPointIndex++;
            if (_currentPathPointIndex >= _navMeshPath.corners.Length)
                PathCalculated = false;
            else 
                currentPoint = _navMeshPath.corners[_currentPathPointIndex];

            return currentPoint;
        }
        public float DistanceToTargetPoint(Vector3 position) => (_targetHit.position - position).magnitude;
    }
}
