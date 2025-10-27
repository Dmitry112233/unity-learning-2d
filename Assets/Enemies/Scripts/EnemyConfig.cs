using UnityEngine;

namespace Enemies.Scripts
{
    [System.Serializable]
    public class EnemyConfigBase
    {
        public EnemyStrength strength;
        public GameObject prefab;
        public float speed;
        public float attackPower;
        public float patrolDistance;
    }

    [System.Serializable]
    public class FlyingEnemyConfig : EnemyConfigBase
    {
        public float amplitude;
    }

    public enum EnemyStrength
    {
        Easy,
        Normal,
        Hard
    }
}