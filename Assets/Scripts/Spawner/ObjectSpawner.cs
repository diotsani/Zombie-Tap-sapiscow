using Sapi.ZombieTap.Objects;
using Sapi.ZombieTap.Status;
using System.Collections.Generic;
using UnityEngine;

namespace Sapi.ZombieTap.Spawner
{    
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Dependency")]
        [SerializeField] private LifeCounter _lifeCounter;
        [SerializeField] private ScoreCounter _scoreCounter;

        [Header("Prefab")]
        [SerializeField] private ObjectZombieNormal _zombieNormalPrefab;
        [SerializeField] private ObjectZombieZigzag _zombieZigzagPrefab;
        [SerializeField] private ObjectHuman _humanPrefab;

        [Header("Config")]
        [SerializeField] private float _spawnRandomRadiusX = 5f;
        [SerializeField] private float _spawnRadiusY = 5.5f;
        [SerializeField] private float _spawnDelay = 2f;

        private bool _isRunning;
        private int _spawnCounter;
        private int _despawnCounter;
        private float _spawnDelayTimer;

        private List<ObjectZombieNormal> _zombieNormalPools = new List<ObjectZombieNormal>();
        private List<ObjectZombieZigzag> _zombieZigzagPools = new List<ObjectZombieZigzag>();
        private List<ObjectHuman> _humanPools = new List<ObjectHuman>();

        public event System.Action OnSpawnFinished;

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            _spawnDelayTimer += Time.deltaTime;
            if (_spawnDelayTimer > _spawnDelay)
            {
                SpawnRandomObject();
                _spawnDelayTimer = 0f;

                if (--_spawnCounter < 0)
                {
                    _isRunning = false;
                }
            }
        }

        public void StartSpawnObject(int spawnTarget)
        {
            _spawnCounter = _despawnCounter = spawnTarget;
            _isRunning = true;
        }

        private void UpdateDespawnCounter()
        {
            if (--_despawnCounter < 0 && !_isRunning)
            {
                OnSpawnFinished?.Invoke();
            }
        }

        private void SpawnRandomObject()
        {
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0: SpawnZombieNormal(); break;
                case 1: SpawnZombieZigzag(); break;
                case 2: SpawnHuman(); break;
            }
        }

        private void SpawnZombieNormal()
        {
            ObjectZombieNormal zombie = _zombieNormalPools.Find(z => !z.gameObject.activeSelf);
            if (zombie == null)
            {
                zombie = InstantiateObject(_zombieNormalPrefab, _zombieNormalPools);
            }

            ConfigSpawnedObject(zombie);
        }

        private void SpawnZombieZigzag()
        {
            ObjectZombieZigzag zombie = _zombieZigzagPools.Find(z => !z.gameObject.activeSelf);
            if (zombie == null)
            {
                zombie = InstantiateObject(_zombieZigzagPrefab, _zombieZigzagPools);
            }

            ConfigSpawnedObject(zombie);
        }

        private void SpawnHuman()
        {
            ObjectHuman human = _humanPools.Find(h => !h.gameObject.activeSelf);
            if (human == null)
            {
                human = InstantiateObject(_humanPrefab, _humanPools);
            }

            ConfigSpawnedObject(human);
        }

        private T InstantiateObject<T>(T prefab, List<T> pool) where T : BaseObject
        {
            T baseObject = Instantiate(prefab, transform);
            baseObject.SetDependency(_lifeCounter, _scoreCounter);
            baseObject.OnDespawned += UpdateDespawnCounter;
            pool.Add(baseObject);

            return baseObject;
        }

        private void ConfigSpawnedObject(BaseObject baseObject)
        {
            baseObject.transform.position = new Vector2(
                Random.Range(-_spawnRandomRadiusX, _spawnRandomRadiusX), _spawnRadiusY
            );
            baseObject.SetDespawnedHeight(-_spawnRadiusY);
            baseObject.gameObject.SetActive(true);
        }
    }
}