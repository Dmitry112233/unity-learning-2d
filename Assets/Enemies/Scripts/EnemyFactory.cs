using System;
using System.Collections.Generic;
using System.Linq;
using CommonScripts;
using Player.Scripts.Bullet;
using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<EnemyConfigBase> groundEnemyConfigs;
        [SerializeField] private List<FlyingEnemyConfig> flightEnemyConfigs;
        
        Dictionary<(EnemyType, EnemyStrength), PoolObject> _enemyPools;

        private void Start()
        {
            _enemyPools = new Dictionary<(EnemyType, EnemyStrength), PoolObject>();
        }

        public BaseEnemyController Create(EnemyType type, EnemyStrength strength, Vector3 position)
        {
            EnemyConfigBase selectedConfig = null;
        
            switch (type)
            {
                case EnemyType.GroundEnemy:
                    selectedConfig = groundEnemyConfigs.FirstOrDefault(config => config.strength == strength);
                    break;
                case EnemyType.AirEnemy:
                    selectedConfig = flightEnemyConfigs.FirstOrDefault(config => config.strength == strength);
                    break;
            }
        
            if (selectedConfig == null)
            {
                Debug.LogError($"No config found for {type} with {strength}");
                return null;
            }
            
            var key = (type, strength);
            
            if (!_enemyPools.TryGetValue(key, out var pool))
            {
                pool = CreatePool(selectedConfig.prefab, key);
                _enemyPools[key] = pool;
            }
            
            var enemy = pool.GetObject(position) as MonoBehaviour;
        
            if (enemy.TryGetComponent<IEnemy>(out var enemyController))
            {
                enemyController.Initialize(selectedConfig);
            }
            else
            {
                Debug.LogWarning($"Enemy prefab {enemy.name} does not implement IEnemy");
            }

            return enemy.GetComponent<BaseEnemyController>();
        }

        private PoolObject CreatePool(GameObject prefab, (EnemyType type, EnemyStrength strength) key)
        {
            var poolGo = new GameObject($"{key.Item1}_{key.Item2}_Pool");
            poolGo.transform.SetParent(transform);
            
            var pool = poolGo.AddComponent<PoolObject>();
            pool.Initialize(prefab, poolGo.transform, initialSize: 10);
            return pool;
        }
    }



    public enum EnemyType
    {
        AirEnemy,
        GroundEnemy
    }
}