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
                        DepthFirstSearch(startWaypoint);
                        Debug.Log("Calculating path with DFS");
                        break;
                    case Algorithm.DIJKSTRA:
                        throw new NotImplementedException();
                    default:
                        BreadthFirstSearch();
                        Debug.Log("Calculating path with BFS");
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
            Debug.Log(endWaypoint);

            Waypoint previous = endWaypoint.ExploredFrom;
            while (previous != startWaypoint)
            {
                Debug.Log(previous);
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

        private bool DepthFirstSearch(Waypoint at)
        {
            if (at.IsExplored) return false;
            at.IsExplored = true;

            List<Waypoint> neighbours = GetNeighbours(at);
            foreach (Waypoint neighbour in neighbours)
            {
                if (neighbour.IsExplored) continue;
                if (neighbour == endWaypoint)
                {
                    endWaypoint.ExploredFrom = at;
                    endWaypoint.IsExplored = true;
                    return true;
                }

                neighbour.ExploredFrom = at;
                if (DepthFirstSearch(neighbour)) return true;
            }
            return false;
        }

        private void BreadthFirstSearch()
        {
            Queue<Waypoint> queue = new Queue<Waypoint>();
            queue.Enqueue(startWaypoint);
            startWaypoint.IsExplored = true;

            while (queue.Count > 0)
            {
                var searchCenter = queue.Dequeue();
                if (searchCenter == endWaypoint) break;
                List<Waypoint> neighbours = GetNeighbours(searchCenter);
                foreach (Waypoint neighbour in neighbours)
                {
                    if (neighbour.IsExplored) continue;
                    queue.Enqueue(neighbour);
                    neighbour.ExploredFrom = searchCenter;
                    neighbour.IsExplored = true;
                }
            }
        }

        private List<Waypoint> GetNeighbours(Waypoint center)
        {
            List<Waypoint> neighbours = new List<Waypoint>();
            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbour = center.GridPosition + direction;
                if (grid.ContainsKey(neighbour))
                {
                    neighbours.Add(grid[neighbour]);
                }
            }
            return neighbours;
        }
        #endregion
    }
}
