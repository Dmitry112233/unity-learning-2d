using CommonScripts;
using Enemies.Scripts;
using Player.Scripts.Bullet;
using UnityEngine;

namespace Enemies.AirEnemy.Scripts
{
    public class FlyingBaseEnemyController : BaseEnemyController
    {
        [SerializeField]
        private float speedMultiplier = 0.1f;
        
        private float _amplitude = 5;
    
        private SpriteRenderer _spriteRenderer;
        private float _lastX;

        void Update()
        {
            _localTime += Time.deltaTime;
            Move();
            Rotate();
        }

        public override void Initialize(PoolObject pool)
        {
            base.Initialize(pool);
            _spriteRenderer =  GetComponent<SpriteRenderer>();
        }
        
        public override void Reset()
        {
            base.Reset();
            _startPosition = Vector3.zero;
        }

        public override void Initialize(EnemyConfigBase config)
        {
            base.Initialize(config);

            if (config is FlyingEnemyConfig flyingConfig)
            {
                _amplitude = flyingConfig.amplitude;
            }
            else
            {
                Debug.LogWarning("Wrong config type passed to FlyingEnemyController");
            }
        }

        private void Move()
        {
            float t = _localTime * _speed * speedMultiplier;
            float offsetX = Mathf.Sin(t) * _patrolDistance;
            float offsetY = Mathf.Sin(t * 2f) * _amplitude;
            transform.position = _startPosition + new Vector3(offsetX, offsetY, 0);
        }

        private void Rotate()
        {
            if (transform.position.x > _lastX)
                _spriteRenderer.flipX = true;
            else if (transform.position.x < _lastX)
                _spriteRenderer.flipX = false; 

            _lastX = transform.position.x;
        }
    }
}
