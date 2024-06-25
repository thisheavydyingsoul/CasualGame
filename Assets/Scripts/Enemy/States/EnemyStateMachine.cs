using CasualGame.FSM;
using UnityEngine;
using System.Collections.Generic;
namespace CasualGame.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5;
        public bool HPToRunAway = false;
        public EnemyStateMachine(EnemyDirectionController enemyDirectionController, 
            NavMesher navMesher, EnemyTarget target,
            float minRunAwayChancePercent, float maxRunAwayChancePercent, float runAwaySpeedDiff)
        {
            var idleState = new IdleState(enemyDirectionController, target);
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var runAwayState = new RunAwayState(target, enemyDirectionController, runAwaySpeedDiff);
            SetInitialState(idleState);
            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition(
                    runAwayState,
                    () => target.DistanceToPlayer() < NavMeshTurnOffDistance &&
                    HPToRunAway && Random.Range(0, 100) <= Random.Range(minRunAwayChancePercent, maxRunAwayChancePercent)),
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    moveForwardState, 
                    () => target.Closest != null && target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                
            });

            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwardState,
                    () => target.Closest != null && target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
            });

            AddState(state: moveForwardState, transitions: new List<Transition>
            { 
                new Transition(
                    runAwayState,
                    () => target.DistanceToPlayer() < NavMeshTurnOffDistance 
                    && HPToRunAway && Random.Range(0, 100) <= Random.Range(minRunAwayChancePercent, maxRunAwayChancePercent)),
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
               
            });

            AddState(state: runAwayState, transitions: new List<Transition>
            {
                 new Transition(
                    idleState,
                    () => target.IsClosestPlayer())
            });
        }
    }
}
