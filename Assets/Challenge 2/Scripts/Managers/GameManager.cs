using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private float timer = 60f;
        public static GameManager Instance { get; private set; }

        public ScoreManager ScoreManager => scoreManager;

        public bool IsGameOver => _isGameOver;
        private bool _isGameOver = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateTimerText();
            gameOverScreen.SetActive(false);
            
            playAgainButton.onClick.AddListener(RestartGame);
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                EndGame();
            }

            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            timerText.text = "Time Left: " + Mathf.CeilToInt(timer) + "s";
        }

        private void EndGame()
        {
            _isGameOver = true;
            gameOverScreen.SetActive(true);
            finalScoreText.text = "Final Score: " + scoreManager.Score;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
