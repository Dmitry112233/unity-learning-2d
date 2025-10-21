using UnityEngine;

namespace Enemies.AirEnemy.Scripts
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float distance = 5f;
        [SerializeField] private float speed = 100f;
    
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

        private void Move()
        {
            float t = Time.time * speed;
            float offset = Mathf.Sin(t) * distance;
            transform.position = _startPosition + Vector3.right * offset;
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
