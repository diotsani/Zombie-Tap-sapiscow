using DG.Tweening;
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

            // Tweening Effect
            DOTween.Kill(_scoreText?.transform);
            _scoreText.transform.localScale = Vector3.one * 1.25f;
            _scoreText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack, 2f);

            _scoreText.SetText("Score: " + _score);
        }
    }
}