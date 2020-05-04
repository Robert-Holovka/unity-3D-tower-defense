using Scripts.Cube;
using Scripts.Enemy;
using UnityEngine;

namespace Scripts.Player
{
    internal class Turret : MonoBehaviour
    {
        [SerializeField] Transform objectToPan = default;
        [SerializeField] ParticleSystem bullets = default;
        [Header("Turret Specifications")]
        [SerializeField] float attackRange = 20f;
        [SerializeField] float damage = 5f;
        public float Damage
        {
            get => damage;
        }

        private Transform enemyTarget = default;
        public Waypoint baseWaypoint = default;

        void Update()
        {
            SetTargetEnemey();
            if (enemyTarget == null)
            {
                Shoot(false);
            }
            else
            {
                float distanceToEnenmy = Vector3.Distance(transform.position, enemyTarget.transform.position);
                Shoot(distanceToEnenmy <= attackRange);
            }
        }

        private void SetTargetEnemey()
        {
            EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
            if (enemies.Length == 0) return;

            enemyTarget = enemies[0].transform;
            for (int i = 1; i < enemies.Length; i++)
            {
                enemyTarget = GetClosest(enemyTarget, enemies[i].transform);
            }
        }

        private Transform GetClosest(Transform transformA, Transform transformB)
        {
            var distanceToA = Vector3.Distance(transform.position, transformA.position);
            var distanceToB = Vector3.Distance(transform.position, transformB.position);
            return (distanceToA <= distanceToB) ? transformA : transformB;
        }

        private void Shoot(bool inRange)
        {
            if (inRange)
            {
                objectToPan.LookAt(enemyTarget.transform);
            }
            var emissionModule = bullets.emission;
            emissionModule.enabled = inRange;
        }
    }
}
