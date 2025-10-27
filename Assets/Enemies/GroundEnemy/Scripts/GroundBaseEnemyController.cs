using Enemies.Scripts;
using UnityEngine;

namespace Enemies.GroundEnemy.Scripts
{
    public class GroundBaseEnemyController : BaseEnemyController
    {
        [SerializeField] private float speedMultiplier = 0.1f;
    
        private Vector3 _startPosition;
        private float _lastX;
        
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _spriteRenderer =  GetComponent<SpriteRenderer>();
            _rb =  GetComponent<Rigidbody2D>();
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

        private void Move()
        {
            float t = Time.time * _speed * speedMultiplier;
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
