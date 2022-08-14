using UnityEngine;

namespace Sapi.ZombieTap.Objects
{
    public abstract class BaseZombieObject : BaseObject
    {
        [Header("Zombie Config")]
        [SerializeField] protected int _damage = 1;

        protected override void CheckPosition()
        {
            if (transform.position.y < _despawnedHeight)
            {
                _lifeCounter.ReduceLife(_damage);
                gameObject.SetActive(false);
            }
        }

        public override void OnRaycasted()
        {
            if (_isDespawning)
            {
                return;
            }

            _scoreCounter.AddScore(_point);
            Despawn();
        }
    }
}