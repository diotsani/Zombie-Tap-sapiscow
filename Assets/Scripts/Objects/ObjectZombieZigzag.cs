using UnityEngine;

namespace Sapi.ZombieTap.Objects
{
    public class ObjectZombieZigzag : BaseZombieObject
    {
        [SerializeField] private float _sideSpeed = 2f;
        [SerializeField] private float _changeSideDelay = 2f;

        private int direction;

        private float _changeSideDelayTimer;

        private void OnEnable()
        {
            _changeSideDelayTimer = _changeSideDelay;
        }

        protected override void Move()
        {
            _changeSideDelayTimer += Time.deltaTime;
            if (_changeSideDelayTimer > _changeSideDelay)
            {
                direction = Random.value > 0.5f ? -1 : 1;
                _changeSideDelayTimer = 0f;
            }

            transform.Translate(
                new Vector3(_sideSpeed * direction, -_moveSpeed, 0f) * Time.deltaTime
            );
        }
    }
}