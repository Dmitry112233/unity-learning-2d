using System;
using Player.Scripts.Bullet;
using UnityEngine;

namespace Player.Scripts.Player
{
    public class FireComponent : MonoBehaviour
    {
        [SerializeField] private PoolObject bulletPool;

        private int _currentDirection = 1;
        private MovementComponent _movementComponent;

        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
        }
        
        private void OnEnable()
        {
            if (_movementComponent != null)
            {
                _currentDirection = _movementComponent.GetCurrentDirection();
                _movementComponent.OnDirectionChanged += HandleDirectionChanged;
            }
        }
        
        private void OnDisable()
        {
            if (_movementComponent != null)
                _movementComponent.OnDirectionChanged -= HandleDirectionChanged;
        }
        
        private void HandleDirectionChanged(int dir)
        {
            _currentDirection = dir;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = bulletPool.GetBullet() as BulletController;
                bullet?.ApplyShoot(_currentDirection);
            }
        }
    }
}