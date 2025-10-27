using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory factory;
        
        [SerializeField] private List<Vector3> flyingEnemyPositionsEasy;
        [SerializeField] private List<Vector3> flyingEnemyPositionsNormal;
        [SerializeField] private List<Vector3> flyingEnemyPositionsHard;
        
        [SerializeField] private List<Vector3> groundEnemyPositionsEasy;
        [SerializeField] private List<Vector3> groundEnemyPositionsNormal;
        [SerializeField] private List<Vector3> groundEnemyPositionsHard;
        
        private void Start()
        {
            flyingEnemyPositionsEasy.ForEach(position => factory.Create(EnemyType.AirEnemy, EnemyStrength.Easy, position));
            flyingEnemyPositionsNormal.ForEach(position => factory.Create(EnemyType.AirEnemy, EnemyStrength.Normal, position));
            flyingEnemyPositionsHard.ForEach(position => factory.Create(EnemyType.AirEnemy, EnemyStrength.Hard, position));
            
            groundEnemyPositionsEasy.ForEach(position => factory.Create(EnemyType.GroundEnemy, EnemyStrength.Easy, position));
            groundEnemyPositionsNormal.ForEach(position => factory.Create(EnemyType.GroundEnemy, EnemyStrength.Normal, position));
            groundEnemyPositionsHard.ForEach(position => factory.Create(EnemyType.GroundEnemy, EnemyStrength.Hard, position));
        }
    }
}