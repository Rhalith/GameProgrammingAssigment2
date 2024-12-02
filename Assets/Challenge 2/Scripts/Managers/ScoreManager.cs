using UnityEngine;
using TMPro;

namespace Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TMP text component
        private int _score;

        public void AddScore(int i)
        {
            _score += i * 10;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = "Score: " + _score;
        }
    }
}