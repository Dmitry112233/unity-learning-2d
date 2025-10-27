using System.Collections.Generic;
using UnityEngine;

namespace Player.Scripts.Bullet
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialPoolSize;
        [SerializeField] private Transform spawnPoint;
        
        public Transform SpawnPoint => spawnPoint;

        private Queue<IPoolable> _pool = new ();

        private void Start()
        {
            if (prefab.GetComponent<IPoolable>() == null)
            {
                Debug.LogError($"{prefab.name} does not implement IPoolable!");
                enabled = false;
                return;
            }
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewObject();
            }
        }

        private void CreateNewObject()
        {
            var obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform).GetComponent<IPoolable>();
            _pool.Enqueue(obj);
        }

        public IPoolable GetBullet()
        {
            if (_pool.Count == 0)
            {
                CreateNewObject();
            }
            
            IPoolable obj = _pool.Dequeue();
            obj.Initialize(this);
            return obj;
        }

        public void ReturnObject(IPoolable obj)
        {
            obj.Reset();
            _pool.Enqueue(obj);
        }
    }
}