using CommonScripts;
using Enemies.Scripts;
using UnityEngine;

namespace Enemies.GroundEnemy.Scripts
{
    public class GroundBaseEnemyController : BaseEnemyController
    {
        [SerializeField] private float speedMultiplier = 0.1f;
    
        private float _lastX;
        
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        

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
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = Vector2.zero;
        }

        public override void Reset()
        {
            base.Reset();
            _rb.velocity = Vector2.zero;
        }

        private void Move()
        {
            float t = _localTime * _speed * speedMultiplier;
            float offset = Mathf.Sin(t) * _patrolDistance;
            
            _rb.velocity = new Vector3(offset, _rb.velocity.y, 0);
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
