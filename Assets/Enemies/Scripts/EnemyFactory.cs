using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<EnemyConfigBase> groundEnemyConfigs;
        [SerializeField] private List<FlyingEnemyConfig> flightEnemyConfigs;

        private GameObject _parentAllEnemies;

        private void Start()
        {
            _parentAllEnemies = new GameObject("AllEnemies");
        }

        public GameObject Create(EnemyType type, EnemyStrength strength, Vector3 position)
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
        
            GameObject enemy = Instantiate(selectedConfig.prefab, position, Quaternion.identity);
        
            enemy.transform.parent = _parentAllEnemies.transform;
        
            if (enemy.TryGetComponent<IEnemy>(out var enemyController))
            {
                enemyController.Initialize(selectedConfig);
            }
            else
            {
                Debug.LogWarning($"Enemy prefab {enemy.name} does not implement IEnemy");
            }

            return enemy;
        }
    }



    public enum EnemyType
    {
        AirEnemy,
        GroundEnemy
    }
}