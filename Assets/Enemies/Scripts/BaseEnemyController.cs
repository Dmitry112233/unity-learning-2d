using UnityEngine;

namespace Enemies.Scripts
{
    public abstract class BaseEnemyController : MonoBehaviour, IEnemy
    {
        protected float _speed;
        protected float _attackPower;
        protected float _patrolDistance;
        
        public virtual void Initialize(EnemyConfigBase config)
        {
            _speed = config.speed;
            _attackPower = config.attackPower;
            _patrolDistance = config.patrolDistance;
            
            gameObject.SetActive(true);
        }
    }
}