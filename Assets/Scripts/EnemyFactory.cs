using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject airEnemyPrefab;
    [SerializeField] private GameObject groundEnemyPrefab;
    
    public GameObject GetEnemyPrefab(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.AirEnemy:
                return airEnemyPrefab;
            case EnemyType.GroundEnemy:
                return groundEnemyPrefab;
            default:
                Debug.LogWarning($"Unknown enemy type: {type}");
                return null;
        }
    }
}

public enum EnemyType
{
    AirEnemy,
    GroundEnemy
}
