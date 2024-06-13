using System;
using UnityEngine;

namespace CasualGame.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;

        private Weapon _weapon;

        private Collider[] _colliders = new Collider[2];
        private float _nextShootTimerSec;
        private GameObject _target;

        void Update()
        {
            _target = GetTarget();
            _nextShootTimerSec -= Time.deltaTime;
            if (_nextShootTimerSec < 0)
            {
                if (HasTarget)
                    _weapon.Shoot(TargetPosition);

                _nextShootTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);
            _weapon = Instantiate(weaponPrefab, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;
            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var mask = LayerUtils.IsEnemy(gameObject) ? LayerUtils.PlayerMask : LayerUtils.EnemyMask;
            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            if (size > 0)
            {
                target = _colliders[0].gameObject;
            }
            return target;
        }
    }
}