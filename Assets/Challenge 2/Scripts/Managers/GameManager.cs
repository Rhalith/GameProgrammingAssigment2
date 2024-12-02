using UnityEngine;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        public static GameManager Instance { get; private set; }

        public ScoreManager ScoreManager => _scoreManager;

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
        
        
    }
}