using Sapi.ZombieTap.Spawner;
using Sapi.ZombieTap.Status;
using UnityEngine;

namespace Sapi.ZombieTap.Wave
{
    public class WaveControl : MonoBehaviour
    {
        [Header("Dependency")]
        [SerializeField] private ObjectSpawner _spawner;

        [Header("Config")]
        [SerializeField] private int _objectPerWave = 10;
        [SerializeField] private float _delayPerWave = 2f;

        private int _waveIndex;
        private bool _isRunning;
        private float _delayPerWaveTimer;

        private void Start()
        {
            _spawner.OnSpawnFinished += () => _isRunning = false;
        }

        private void Update()
        {
            if (_isRunning)
            {
                return;
            }

            _delayPerWaveTimer += Time.deltaTime;
            if (_delayPerWaveTimer > _delayPerWave)
            {
                _spawner.StartSpawnObject(_objectPerWave);
                _delayPerWaveTimer = 0f;
                _isRunning = true;
            }
        }
    }
}