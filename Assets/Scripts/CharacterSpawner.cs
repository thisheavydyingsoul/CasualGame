
using CasualGame.Camera;
using CasualGame.Enemy;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CasualGame.Scripts
{
    public class CharacterSpawner : MonoBehaviour
    {
        private static readonly List<CharacterSpawner> _spawners = new List<CharacterSpawner>();

        [SerializeField]
        private float _range = 2f;
        [SerializeField]
        private EnemyCharacter _enemyPrefab;

        [SerializeField]
        private PlayerCharacter _playerPrefab;

        [SerializeField]
        private float _minSpawnIntervalSeconds = 20;

        [SerializeField]
        private float _maxSpawnIntervalSeconds = 60;
        
        private LevelOverseer _levelOverseer;
        private float _currentSpawnIntervalSeconds;
        private float _currentSpawnTimerSeconds;

        public float Range { get => _range; set => _range = value; }

        protected void Awake()
        {
            _levelOverseer = FindAnyObjectByType<LevelOverseer>();
            _spawners.Add(this);
        }

        protected void Update()
        {
            if (_levelOverseer.StopSpawn)
                return;
            _currentSpawnTimerSeconds += Time.deltaTime;
            if (_currentSpawnTimerSeconds <= _currentSpawnIntervalSeconds)
                return;
            _currentSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            _currentSpawnTimerSeconds = 0f;
            var randomPointInsideRange = Random.insideUnitCircle * Range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;
            Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, transform);
            _levelOverseer.EnemySpawned();
        }

        public static void SpawnPlayer()
        {
            if (_spawners.Count == 0)
                return;
            CharacterSpawner spawner = _spawners[Random.Range(0, _spawners.Count)];
            var randomPointInsideRange = Random.insideUnitCircle * spawner.Range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + spawner.transform.position;
            PlayerCharacter player = Instantiate(spawner._playerPrefab, randomPosition, Quaternion.identity, spawner.transform);
            CameraController cameraController = FindObjectOfType<CameraController>();
            cameraController.SetPlayer(player);
        }

        protected void OnDrawGizmos()
        {
            var cachedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, Range);
            Handles.color = cachedColor;
        }

    }
}