using Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    internal class PlayerHealth : MonoBehaviour
    {
        [SerializeField] Text healthText = default;
        [SerializeField] float health = 100f;

        private void Start()
        {
            healthText.text = $"Health: {health}";
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            health -= enemy.Damage;
            healthText.text = health.ToString();
        }
    }
}

