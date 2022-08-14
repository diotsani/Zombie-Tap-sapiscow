using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Sapi.ZombieTap.Status
{
    public class LifeCounter : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private GameObject _gameOverInfo;

        [Header("Config")]
        [SerializeField] private int _maxLife = 3;

        private int _life;

        public bool IsDead { get; private set; }

        public event System.Action OnDead;

        private void Awake()
        {
            SetLife(_maxLife);
        }

        public void AddLife(int value) => SetLife(_life + value);

        public void ReduceLife(int value)
        {
            SetLife(_life - value);
            if (_life <= 0)
            {
                IsDead = true;
                _gameOverInfo.SetActive(true);

                OnDead?.Invoke();
            }
        }

        public void ForceDead() => ReduceLife(_life);

        private void SetLife(int life)
        {
            _life = life;

            // Tweening Effect
            DOTween.Kill(_lifeText?.transform);
            _lifeText.transform.localScale = Vector3.one * 1.25f;
            _lifeText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack, 2f);

            _lifeText.SetText("Life: " + _life);
        }
    }
}