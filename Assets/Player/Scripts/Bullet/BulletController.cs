using System.Collections;
using UnityEngine;

namespace Player.Scripts.Bullet
{
    public class BulletController : MonoBehaviour , IPoolable
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float angle = 45f;
    
        private Rigidbody2D _rigidbody;
        private Coroutine _lifeCoroutine;
        private PoolObject _pool;

        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void ApplyShoot(int currentDirection)
        {
            var direction = currentDirection < 0 ? Vector2.left : Vector2.right;
            float appliedAngle = currentDirection < 0 ? -angle : angle;
            
            Vector2 directionUpdated = Quaternion.Euler(0, 0, appliedAngle) * direction;
            _rigidbody.AddForce(directionUpdated * speed, ForceMode2D.Impulse);
        }

        public void Initialize(PoolObject pool)
        {
            _pool = pool;
            gameObject.SetActive(true);
            _rigidbody.velocity = Vector2.zero;
            
            if (_lifeCoroutine != null)
                StopCoroutine(_lifeCoroutine);
            _lifeCoroutine = StartCoroutine(LifeTimer());
        }

        public void Reset()
        {
            gameObject.SetActive(false);
            _rigidbody.velocity = Vector2.zero;
            transform.rotation = Quaternion.identity;
            transform.position = _pool.SpawnPoint.position;
        }
        
        private IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(5f);
            _pool.ReturnObject(this);
        }
        
        void OnDisable()
        {
            if (_lifeCoroutine != null)
                StopCoroutine(_lifeCoroutine);
        }
    }
}
