using System;
using CommonScripts;
using UnityEngine;

namespace Enemies.Scripts
{
    public abstract class BaseEnemyController : MonoBehaviour, IEnemy, IPoolable
    {
        protected float _speed;
        protected float _attackPower;
        protected float _patrolDistance;
        protected PoolObject _pool;
        protected float _localTime;
        protected Vector3 _startPosition;
        
        public event Action<BaseEnemyController> OnEnemyDied;
        
        
        public virtual void Initialize(EnemyConfigBase config)
        {
            _speed = config.speed;
            _attackPower = config.attackPower;
            _patrolDistance = config.patrolDistance;
        }

        public virtual void Initialize(PoolObject pool)
        {
            _pool = pool;
            _startPosition = transform.position;
            _localTime = 0f;
            gameObject.SetActive(true);
        }

        public virtual void Reset()
        {
            transform.rotation = Quaternion.identity;
            _localTime = 0f;
            gameObject.SetActive(false);
        }
        
        public virtual void TakeHit()
        {
            OnEnemyDied?.Invoke(this);
            _pool.ReturnObject(this);
        }
    }
}