using System.Collections;
using UnityEngine;
using TMPro;

namespace Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TMP text component
        private int _score;
        private int _scoreMultiplier = 1;

        public int Score => _score;

        public void AddScore(int i)
        {
            if (i == 0)
            {
                SelectPowerUpEffect();
                _score += 50 * _scoreMultiplier;
            }
            else
            {
                _score += i * 10 * _scoreMultiplier;
            }
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = "Score: " + _score;
        }

        private void SelectPowerUpEffect()
        {
            int random = Random.Range(0, 2);
            switch (random)
            {
                case 0:
                    StartCoroutine(MultiplierTimer());
                    break;
                case 1:
                    GameManager.Instance.SlowDownBallSpawn();
                    break;
            }
        }
        
        private IEnumerator MultiplierTimer()
        {
            _scoreMultiplier = 2;
            yield return new WaitForSeconds(5);
            _scoreMultiplier = 1;
        }
    }
}