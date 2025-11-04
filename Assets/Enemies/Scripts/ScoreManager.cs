using System;
using UnityEngine;

namespace Enemies.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private EnemyManager manager;
        
        private int _kills;
        
        public int Kills => _kills;

        private void OnEnable()
        {
            manager.OnEnemyDied += HandleEnemyDied;
        }
        
        private void OnDisable()
        {
            manager.OnEnemyDied -= HandleEnemyDied;
        }

        private void HandleEnemyDied(BaseEnemyController enemy)
        {
            _kills++;
            Debug.Log($"Total kills: {_kills}");
        }
    }
}