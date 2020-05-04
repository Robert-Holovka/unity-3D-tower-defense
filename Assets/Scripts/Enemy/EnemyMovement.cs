using Scripts.Cube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    internal class EnemyMovement : MonoBehaviour
    {
        [SerializeField] float movementPeriod = 0.5f;

        private void Start()
        {
            PathFinder pathFinder = FindObjectOfType<PathFinder>();
            List<Waypoint> path = pathFinder.GetPath();
            StartCoroutine(FollowPath(path));
        }

        private IEnumerator FollowPath(List<Waypoint> path)
        {
            foreach (Waypoint waypoint in path)
            {
                yield return new WaitForSeconds(movementPeriod);
                Vector3 waypointPosition = waypoint.transform.position;
                transform.position = new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z);
            }
            yield return new WaitForSeconds(movementPeriod);

            // Reached destination
            gameObject.SetActive(false);
        }
    }
}
