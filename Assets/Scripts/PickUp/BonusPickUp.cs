using System.Collections;
using UnityEngine;

namespace CasualGame.PickUp
{
    public abstract class BonusPickUp : ItemPickUp
    {
        public abstract void EndBonus(PlayerCharacter playerCharacter);
    }
}