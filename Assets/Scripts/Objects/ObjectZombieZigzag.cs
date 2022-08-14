using UnityEngine;

namespace Sapi.ZombieTap.Objects
{
    public class ObjectZombieZigzag : BaseZombieObject
    {
        [Header("Zigzag Zombie Config")]
        [SerializeField] private float _sideSpeed = 2f;
        [SerializeField] private float _changeSideDelay = 2f;

        private int _direction;
        private float _sideBound;
        private float _changeSideDelayTimer;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _changeSideDelayTimer = _changeSideDelay;
        }

        protected override void Move()
        {
            // Change direction within specific duration
            _changeSideDelayTimer += Time.deltaTime;
            if (_changeSideDelayTimer > _changeSideDelay)
            {
                _direction = Random.value > 0.5f ? -1 : 1;
                _changeSideDelayTimer = 0f;
            }

            // Clamp translation with side bound
            if (transform.position.x > _sideBound) _direction = -1;
            else if (transform.position.x < -_sideBound) _direction = 1;

            transform.Translate(
                new Vector3(_sideSpeed * _direction, -_moveSpeed, 0f) * Time.deltaTime
            );
        }

        public void SetSideBound(float bound)
        {
            _sideBound = bound;
        }
    }
}