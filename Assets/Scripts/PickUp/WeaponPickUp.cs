using CasualGame.Shooting;
using UnityEngine;

namespace CasualGame.PickUp
{
    public class WeaponPickUp : ItemPickUp
    {
        [SerializeField]
        private Weapon _weaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
           base.PickUp(character);
           character.SetWeapon(_weaponPrefab);
        }
    }
}