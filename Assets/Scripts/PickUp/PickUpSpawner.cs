using UnityEditor;
using UnityEngine;

namespace CasualGame.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private ItemPickUp _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        [SerializeField]
        public float _minSpawnIntervalSeconds = 3;

        [SerializeField]
        public float _maxSpawnIntervalSeconds = 10;

        private float _currentSpawnIntervalSeconds;
        private float _currentSpawnTimerSeconds;
        private int _currentCount;

        protected void Update()
        {
            if (_currentCount >= _maxCount)
                return;
            _currentSpawnTimerSeconds += Time.deltaTime;
            if (_currentSpawnTimerSeconds <= _currentSpawnIntervalSeconds)
                return;
            _currentSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
            _currentSpawnTimerSeconds = 0f;
            _currentCount++;
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

            var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
            pickUp.OnPickedUp += OnItemPickedUp;
        }

        private void OnItemPickedUp(ItemPickUp pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }    

        protected void OnDrawGizmos()
        {
            var cachedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cachedColor;
        }
    }
}