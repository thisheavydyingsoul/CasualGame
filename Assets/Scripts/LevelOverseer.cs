using CasualGame.Enemy;
using System;
using UnityEngine;

namespace CasualGame
{
    public class LevelOverseer : MonoBehaviour
    {
        public event Action levelCompleted;
        private int _curEnemyCounter = 0;
        private int _spawnedEnemyCounter = 0;

        [SerializeField]
        private int _maxEnemyCounter = 10;
        public bool StopSpawn { get; private set; } = false;

        protected void Awake()
        {
            _spawnedEnemyCounter = FindObjectsOfType<EnemyCharacter>().Length;
            _curEnemyCounter = _spawnedEnemyCounter;
        }

        public void Update()
        {
            StopSpawn = _spawnedEnemyCounter >= _maxEnemyCounter;
            if (StopSpawn && _curEnemyCounter <= 0)
                levelCompleted?.Invoke();
        }

        public void EnemySpawned() { 
            _spawnedEnemyCounter++; 
            _curEnemyCounter++;
        }

        public void EnemyKilled() => _curEnemyCounter--;

    }
}