using Scripts.Enemy;
using Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts
{
    internal class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject gameOverPanel = default;
        [SerializeField] Text gameOverText = default;

        private PlayerHealth player;
        private EnemySpawner enemySpawner;
        private int enemiesDied = 0;

        private void Awake()
        {
            player = FindObjectOfType<PlayerHealth>();
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        private void OnEnable() => player.OnPlayerDeath += PlayerDied;
        private void OnDisable() => player.OnPlayerDeath -= PlayerDied;
        private void Start() => gameOverPanel.SetActive(false);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnQuitApplication();
            }
        }

        public void PlayAgain()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void OnQuitApplication() => Application.Quit();

        public void OnEnemyDeath()
        {
            enemiesDied++;
            if (enemiesDied == enemySpawner.WaveSize)
            {
                OnGameFinished("VICTORY");
            }
        }

        private void PlayerDied() => OnGameFinished("YOU DIED");

        private void OnGameFinished(string text)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            gameOverText.text = text;
        }
    }
}