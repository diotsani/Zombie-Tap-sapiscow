using TMPro;
using UnityEngine;

namespace Sapi.ZombieTap.Status
{
    public class ScoreCounter : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private TextMeshProUGUI _scoreText;

        private int _score;

        public void AddScore(int value)
        {
            _score += value;
            _scoreText.SetText("Score: " + _score);
        }
    }
}