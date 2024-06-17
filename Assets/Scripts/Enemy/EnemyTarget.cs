
using CasualGame.PickUp;
using CasualGame.Shooting;
using UnityEngine;

namespace CasualGame.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }
        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly Collider[] _colliders = new Collider[10];
        private readonly PlayerCharacter _player;
        public bool weaponChanged = false;
        public bool runAway = false;
        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
        }

        public void FindClosest()
        {
            float minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpMask | LayerUtils.PlayerMask);
            for (int i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;
                var distance = DistanceFromAgentTo(go);
                if (distance >= minDistance) continue;
                if (go.GetComponent<WeaponPickUp>() != null && weaponChanged) continue;
                if (_player != null && go == _player.gameObject && runAway) continue;

                minDistance = distance;
                Closest = go;

            }

            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance && !runAway)
                Closest = _player.gameObject;
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                return DistanceFromAgentTo(Closest);
            return 0;
        }

        public float DistanceToPlayer()
        {
            if (_player != null) return DistanceFromAgentTo(_player.gameObject);
            else return float.MaxValue;
        }

        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agentTransform.position,
                _viewRadius,
                _colliders,
                layerMask);

            return size;
        }
        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;

        public void ResetClosest() => Closest = null;

        public Vector3 PlayerCoordinates()
        {
            if (_player != null)
                return _player.transform.position;
            else return Vector3.zero;
        }

        public Vector3 AgentTransform() => _agentTransform.position;
    } 
}
