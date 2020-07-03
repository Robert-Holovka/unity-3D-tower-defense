using System.Collections;
using UnityEngine;

namespace Scripts.Enemy
{
    internal class EnemySpawner : MonoBehaviour
    {
        [SerializeField] EnemyMovement enemyPrefab = default;
        [SerializeField] Transform parent = default;
        [Range(0.1f, 20f)] [SerializeField] float secondsBetweenSpawns = 2f;
        [SerializeField] int waveSIze = 10;

        public int WaveSize { get => waveSIze; }

        private int remained;

        private void Start()
        {
            remained = waveSIze;
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (remained > 0)
            {
                var newEnemy = Instantiate(enemyPrefab, parent.position, Quaternion.identity);
                newEnemy.transform.parent = parent;
                remained--;
                yield return new WaitForSeconds(secondsBetweenSpawns);
            }
        }
    }
}