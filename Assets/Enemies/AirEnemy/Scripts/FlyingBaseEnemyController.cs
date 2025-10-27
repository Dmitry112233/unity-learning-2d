using Enemies.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.AirEnemy.Scripts
{
    public class FlyingBaseEnemyController : BaseEnemyController
    {
        [SerializeField]
        private float speedMultiplier = 0.1f;
        
        private float _amplitude = 5;
    
        private Vector3 _startPosition;
        private SpriteRenderer _spriteRenderer;
        private float _lastX;

        private void Awake()
        {
            _spriteRenderer =  GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            _startPosition = transform.position;
        }

        void Update()
        {
            Move();
            Rotate();
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
            float t = Time.time * _speed * speedMultiplier;
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
