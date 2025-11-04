using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory factory;

        [Serializable]
        private class EnemySpawnPoints
        {
            public EnemyType type;
            public EnemyStrength strength;
            public List<Vector3> positions;
        }

        [SerializeField] private List<EnemySpawnPoints> spawnPoints = new();

        private Dictionary<(EnemyType, EnemyStrength), List<Vector3>> _spawnDict;

        public event Action<BaseEnemyController> OnEnemySpawned;

        private void Awake()
        {
            _spawnDict = new Dictionary<(EnemyType, EnemyStrength), List<Vector3>>();
            foreach (var sp in spawnPoints)
            {
                _spawnDict[(sp.type, sp.strength)] = sp.positions;
            }
        }

        private void Start()
        {
            foreach (KeyValuePair<(EnemyType, EnemyStrength), List<Vector3>> keyValuePair in _spawnDict)
            {
                var enemy = factory.Create(
                    keyValuePair.Key.Item1, keyValuePair.Key.Item2,
                    keyValuePair.Value[Random.Range(0, keyValuePair.Value.Count)]);
                OnEnemySpawned?.Invoke(enemy);
            }
        }

        public void CreateEnemy(EnemyType type, EnemyStrength strength)
        {
            if (!_spawnDict.TryGetValue((type, strength), out var positions) || positions.Count == 0)
            {
                Debug.LogWarning($"No spawn positions for {type} ({strength})");
                return;
            }

            var spawnPos = positions[Random.Range(0, positions.Count)];
            var enemy = factory.Create(type, strength, spawnPos);
            OnEnemySpawned?.Invoke(enemy);
        }
    }
}