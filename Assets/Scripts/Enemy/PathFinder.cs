using Scripts.Cube;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    internal class PathFinder : MonoBehaviour
    {
        private enum Algorithm
        {
            BFS,
            DFS,
            DIJKSTRA,
            A_STAR
        }
        [SerializeField] Algorithm algorithm;
        [SerializeField] Waypoint startWaypoint = default;
        [SerializeField] Waypoint endWaypoint = default;

        private readonly Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };
        private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
        private List<Waypoint> path = new List<Waypoint>();

        public List<Waypoint> GetPath()
        {
            if (path.Count == 0)
            {
                LoadBlocks();
                switch (algorithm)
                {
                    case Algorithm.A_STAR:
                        throw new NotImplementedException();
                    case Algorithm.DFS:
                        throw new NotImplementedException();
                    case Algorithm.DIJKSTRA:
                        throw new NotImplementedException();
                    default:
                        BreadthFirstSearch();
                        break;
                }
                FormPath();
            }
            return path;
        }

        private void LoadBlocks()
        {
            Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
            foreach (Waypoint wp in waypoints)
            {
                var gridPosition = wp.GridPosition;
                if (grid.ContainsKey(gridPosition))
                {
                    Debug.LogWarning("Overlapping block: " + wp);
                }
                else
                {
                    grid.Add(gridPosition, wp);
                }
            }
        }

        private void FormPath()
        {
            SetAsPath(endWaypoint);

            Waypoint previous = endWaypoint.ExploredFrom;
            while (previous != startWaypoint)
            {
                SetAsPath(previous);
                previous = previous.ExploredFrom;
            }

            SetAsPath(startWaypoint);
            path.Reverse();
        }

        private void SetAsPath(Waypoint waypoint)
        {
            path.Add(waypoint);
            waypoint.IsPlaceable = false;
        }

        #region ALGORITHM
        private void BreadthFirstSearch()
        {
            Queue<Waypoint> queue = new Queue<Waypoint>();
            queue.Enqueue(startWaypoint);
            startWaypoint.IsExplored = true;

            while (queue.Count > 0)
            {
                var searchCenter = queue.Dequeue();
                if (searchCenter == endWaypoint) break;
                ExploreNeighbours(searchCenter, queue);
            }
        }

        private void ExploreNeighbours(Waypoint from, Queue<Waypoint> queue)
        {
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbourCoordinates = from.GridPosition + direction;
                if (!grid.ContainsKey(neighbourCoordinates)) continue;

                Waypoint neighbour = grid[neighbourCoordinates];
                if (!neighbour.IsExplored)
                {
                    queue.Enqueue(neighbour);
                    neighbour.ExploredFrom = from;
                    neighbour.IsExplored = true;
                }
            }
        }
        #endregion
    }
}
