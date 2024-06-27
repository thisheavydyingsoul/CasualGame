
using System;
using UnityEngine;

namespace CasualGame.PickUp
{
    public abstract class ItemPickUp : MonoBehaviour
    {
        public event Action<ItemPickUp> OnPickedUp;
        public virtual void PickUp(BaseCharacter character)
        {
            OnPickedUp?.Invoke(this);
        }

    }
}