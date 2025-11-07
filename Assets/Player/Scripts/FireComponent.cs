using CommonScripts;
using Managers.Scripts;
using Player.Scripts.Bullet;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.Scripts
{
    public class FireComponent : MonoBehaviour
    {
        [SerializeField] private PoolObject bulletPool;
        [SerializeField] private Transform spawnPoint;

        private int _currentDirection = 1;
        private MovementComponent _movementComponent;
        private bool canFire = true;

        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
        }
        
        private void OnEnable()
        {
            PauseManager.OnPaused += HandlePaused;
            PauseManager.OnResumed += HandleResumed;
            
            if (_movementComponent != null)
            {
                _currentDirection = _movementComponent.GetCurrentDirection();
                _movementComponent.OnDirectionChanged += HandleDirectionChanged;
            }
        }
        
        private void OnDisable()
        {
            PauseManager.OnPaused -= HandlePaused;
            PauseManager.OnResumed -= HandleResumed;
            
            if (_movementComponent != null)
                _movementComponent.OnDirectionChanged -= HandleDirectionChanged;
        }
        
        private void HandleDirectionChanged(int dir)
        {
            _currentDirection = dir;
        }

        private void Update()
        {
            if (!canFire) return;
            
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = bulletPool.GetObject(spawnPoint.position) as BulletController;
                bullet?.ApplyShoot(_currentDirection);
            }
        }
        
        private void HandlePaused() => canFire = false;
        private void HandleResumed() => canFire = true;

    }
}