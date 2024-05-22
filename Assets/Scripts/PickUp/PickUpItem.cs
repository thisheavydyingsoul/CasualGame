
using System;
using UnityEngine;

namespace CasualGame.PickUp
{
    public abstract class PickUpItem : MonoBehaviour
    {
        public event Action<PickUpItem> OnPickedUp;
        
        public virtual void PickUp(BaseCharacter character)
        {
            OnPickedUp?.Invoke(this);
        }

        protected void Update()
        {
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time)/10, transform.position.z);
            transform.Rotate(0f, 20 * Time.deltaTime, 0f);
        }
    }
}