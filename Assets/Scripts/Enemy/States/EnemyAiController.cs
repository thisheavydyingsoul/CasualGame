using CasualGame;
using CasualGame.Enemy;
using CasualGame.Enemy.States;
using UnityEngine;

namespace Casualgame.Enemy.States
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;
        [SerializeField]
        public float HPToRunAwayPercent = 30f;
        [SerializeField]
        private float _minRunAwayChancePercent = 30f;
        [SerializeField]
        private float _maxRunAwayChancePercent = 70f;
        [SerializeField]
        private float _runAwaySpeedDiff = 1f;

        private EnemyStateMachine _stateMachine;
        private EnemyTarget _target;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();

            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius);

            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target, 
                _minRunAwayChancePercent, _maxRunAwayChancePercent, _runAwaySpeedDiff);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
        public void HPToRunAway()
        {
            _target.runAway = true;
            _stateMachine.HPToRunAway = true;
        }
        public void WeaponChanged()
        {
            _target.weaponChanged = true;
            _target.ResetClosest();
        }

    }
}