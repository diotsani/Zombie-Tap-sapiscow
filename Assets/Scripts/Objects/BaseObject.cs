using DG.Tweening;
using Sapi.ZombieTap.Status;
using UnityEngine;

namespace Sapi.ZombieTap.Objects
{
    public abstract class BaseObject : MonoBehaviour, IRaycastable
    {
        [Header("Base Config")]
        [SerializeField] protected float _moveSpeed = 2f;
        [SerializeField] protected int _point = 1;

        protected LifeCounter _lifeCounter;
        protected ScoreCounter _scoreCounter;

        protected bool _isDespawning;
        protected float _despawnedHeight;

        public event System.Action OnDespawned;

        protected virtual void OnEnable()
        {
            _isDespawning = false;

            DOTween.Kill(transform);
            transform.localScale = Vector3.one;
        }

        protected virtual void OnDisable()
        {
            OnDespawned?.Invoke();
        }

        protected virtual void Update()
        {
            if (_lifeCounter.IsDead || _isDespawning)
            {
                return;
            }

            Move();
            CheckPosition();
        }

        public void SetDependency(LifeCounter lifeCounter, ScoreCounter scoreCounter)
        {
            _lifeCounter = lifeCounter;
            _scoreCounter = scoreCounter;
        }

        public void SetDespawnedHeight(float despawnedHeight)
        {
            _despawnedHeight = despawnedHeight;
        }

        protected virtual void Move()
        {
            transform.Translate(0f, -_moveSpeed * Time.deltaTime, 0f);
        }

        protected virtual void Despawn()
        {
            _isDespawning = true;
            transform.DOScale(Vector3.zero, 0.25f)
                .SetEase(Ease.InBack, 3f)
                .OnComplete(() => gameObject.SetActive(false)
            );
        }

        protected abstract void CheckPosition();

        public abstract void OnRaycasted();
    }
}