using System.Collections;
using UnityEngine;

namespace CasualGame.PickUp
{
    public class SpeedBonusPickUp : BonusPickUp
    {
        [SerializeField]
        private float _acceleration = 2f;

        [SerializeField]
        private float _timerSeconds = 10f;
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            ((PlayerCharacter)character).PickUpBonus(this, _timerSeconds);
            ((PlayerCharacter)character).Accelerate(_acceleration);
        }

        public override void EndBonus(PlayerCharacter playerCharacter) => playerCharacter.Decelerate(_acceleration);

    }
}