using Player.Scripts.Bullet;

namespace CommonScripts
{
    public interface IPoolable
    {
        void Initialize(PoolObject pool);

        void Reset();
    }
}