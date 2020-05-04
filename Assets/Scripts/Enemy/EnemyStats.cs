using Scripts.Player;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    internal class EnemyStats : MonoBehaviour
    {
        [SerializeField] ParticleSystem deathParticles = default;
        [SerializeField] float health = 10;
        [SerializeField] float damage = 10;
        public float Damage
        {
            get => damage;
        }

        private void OnParticleCollision(GameObject other)
        {
            health -= other.GetComponentInParent<Turret>().Damage;

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            var deathVFX = Instantiate(deathParticles, transform.position, Quaternion.identity);
            deathVFX.Play();
            Destroy(deathVFX.gameObject, deathVFX.main.duration);
            Destroy(gameObject);
        }
    }
}