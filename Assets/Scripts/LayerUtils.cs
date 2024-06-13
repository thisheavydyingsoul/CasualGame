using UnityEngine;

namespace CasualGame
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";
        public const string PickUpLayerName = "PickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);
        public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);

        public static readonly int PlayerMask = LayerMask.GetMask(PlayerLayerName);

        public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName);


        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;

        public static bool IsPickUp(GameObject other) => other.layer == PickUpLayer;

        public static bool IsEnemy(GameObject other) => other.layer == EnemyLayer;

    }
}
