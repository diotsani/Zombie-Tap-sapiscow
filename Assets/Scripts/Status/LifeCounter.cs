using TMPro;
using UnityEngine;

namespace Sapi.ZombieTap.Status
{
    public class LifeCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lifeText;

        [Header("Config")]
        [SerializeField] private int _maxLife = 3;

        private int _life;

        public event System.Action OnDead;

        private void Awake()
        {
            SetLife(_maxLife);
        }

        public void AddLife(int value)
        {
            SetLife(_life + value);
        }

        public void ReduceLife(int value)
        {
            SetLife(_life - value);
            if (_life <= 0)
            {
                Debug.Log("Dead!");
                OnDead?.Invoke();
            }
        }

        public void ForceDead()
        {
            ReduceLife(_life);
        }

        private void SetLife(int life)
        {
            _life = life;
            _lifeText.SetText("Life: " + _life);
        }
    }
}