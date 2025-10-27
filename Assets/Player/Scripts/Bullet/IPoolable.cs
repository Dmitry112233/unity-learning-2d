using UnityEngine;

namespace Player.Scripts.Bullet
{
    public interface IPoolable
    {
        void Initialize(PoolObject pool);

        void Reset();
    }
}