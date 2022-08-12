using UnityEngine;

namespace Sapi.ZombieTap.Objects
{
    public abstract class BaseZombieObject : BaseObject
    {
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
            _scoreCounter.AddScore(_point);
            gameObject.SetActive(false);
        }
    }
}