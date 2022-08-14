namespace Sapi.ZombieTap.Objects
{
    public class ObjectHuman : BaseObject
    {
        protected override void CheckPosition()
        {
            if (transform.position.y < _despawnedHeight)
            {
                _scoreCounter.AddScore(_point);
                gameObject.SetActive(false);
            }
        }

        public override void OnRaycasted()
        {
            if (_isDespawning)
            {
                return;
            }

            _lifeCounter.ForceDead();
            Despawn();
        }
    }
}