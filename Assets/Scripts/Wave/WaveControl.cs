using DG.Tweening;
using Sapi.ZombieTap.Spawner;
using Sapi.ZombieTap.Status;
using TMPro;
using UnityEngine;

namespace Sapi.ZombieTap.Wave
{
    public class WaveControl : MonoBehaviour
    {
        [Header("Dependency")]
        [SerializeField] private ObjectSpawner _spawner;
        [SerializeField] private LifeCounter _lifeCounter;

        [Header("View")]
        [SerializeField] private TextMeshProUGUI _waveText;

        [Header("Config")]
        [SerializeField] private int _objectPerWave = 10;
        [SerializeField] private float _delayPerWave = 2f;

        private int _waveIndex;
        private bool _isRunning;
        private float _delayPerWaveTimer;

        private void Start()
        {
            _waveIndex = 1;
            _spawner.OnSpawnFinished += OnSpawnFinished;
        }

        private void Update()
        {
            if (_isRunning || _lifeCounter.IsDead)
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

        private void OnSpawnFinished()
        {
            _isRunning = false;

            // Tweening Effect
            DOTween.Kill(_waveText?.transform);
            _waveText.transform.localScale = Vector3.one * 2f;
            _waveText.transform.DOScale(Vector3.one, 2f).SetEase(Ease.OutBack, 2f);

            _waveText.SetText("Wave " + (++_waveIndex));
        }
    }
}