using UnityEngine;

namespace CasualGame.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }

    }
}
