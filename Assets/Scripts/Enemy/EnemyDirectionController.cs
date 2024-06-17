using CasualGame.Movement;
using System;
using UnityEngine;

namespace CasualGame.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection { get; private set; }
        public event Action<float> OnStartRunAway;
        public event Action<float> OnStopRunAway;


        public void UpdateMovementDirection(Vector3 targetPosition)
        {
            var realDirection = targetPosition - transform.position;
            MovementDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;
        }

        public void StartRunAway(float runAwaySpeedDiff) => OnStartRunAway?.Invoke(runAwaySpeedDiff);
        
        public void StopRunAway(float runAwaySpeedDiff) => OnStopRunAway?.Invoke(runAwaySpeedDiff);
    }
}