using UnityEngine;

namespace Enemies.GroundEnemy.Scripts
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float distance = 5f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float groundY = 0f;
    
        private Vector3 _startPosition;
        private SpriteRenderer _spriteRenderer;
        private float _lastX;
        
        private readonly float _gravity = -9.81f;
        private float _verticalVelocity = 0f;

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
            ApplyGravity();
            Rotate();
        }

        private void Move()
        {
            float t = Time.time * speed;
            float offset = Mathf.Sin(t) * distance;
            transform.position = new Vector3(_startPosition.x + offset, transform.position.y, transform.position.z);
        }
        
        private void ApplyGravity()
        {
            _verticalVelocity += _gravity * Time.deltaTime;

            transform.position += Vector3.up * (_verticalVelocity * Time.deltaTime);

            if (transform.position.y <= groundY)
            {
                transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
                _verticalVelocity = 0f;
            }
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
