using System.Collections;
using UnityEngine;

namespace CasualGame.PickUp
{
    public class PickUpSpeedBonus : PickUpItem
    {
        [SerializeField]
        private float _acceleration = 2f;

        [SerializeField]
        private float _timerSeconds = 10f;
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            Debug.Log("Speed Bonus Picked Up");
            character.IncreaseSpeed(_acceleration, _timerSeconds);
        }
    }
}