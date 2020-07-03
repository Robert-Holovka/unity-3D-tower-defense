using Scripts.Enemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Player
{
    internal class PlayerHealth : MonoBehaviour
    {
        [SerializeField] Text healthText = default;
        [SerializeField] float health = 100f;

        public event UnityAction OnPlayerDeath;

        private void Start() => healthText.text = $"Health: {health}";

        private void OnTriggerEnter(Collider other)
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            health -= enemy.Damage;
            healthText.text = healthText.text = $"Health: {health}";

            if (health <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }
}

