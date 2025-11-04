using System;
using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner spawner;
        
        public event Action<BaseEnemyController> OnEnemyDied;

        private void OnEnable()
        {
            spawner.OnEnemySpawned += HandleEnemySpawned;
        }

        private void OnDisable()
        {
            spawner.OnEnemySpawned -= HandleEnemySpawned;
        }

        private void HandleEnemySpawned(BaseEnemyController enemy)
        {
            enemy.OnEnemyDied += HandleEnemyDied;
        }

        private void HandleEnemyDied(BaseEnemyController enemy)
        {
            enemy.OnEnemyDied -= HandleEnemyDied;
            
            OnEnemyDied?.Invoke(enemy);
            
            spawner.CreateEnemy(GetRandomEnemyType(), GetRandomEnemyStrength());
            spawner.CreateEnemy(GetRandomEnemyType(), GetRandomEnemyStrength());
        }

        private EnemyType GetRandomEnemyType()
        {
            var values = System.Enum.GetValues(typeof(EnemyType));
            return (EnemyType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }
        
        private EnemyStrength GetRandomEnemyStrength()
        {
            var values = System.Enum.GetValues(typeof(EnemyStrength));
            return (EnemyStrength)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }
    }
}