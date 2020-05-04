using UnityEngine;

namespace Scripts.Cube
{
    [SelectionBase]
    [ExecuteInEditMode]
    internal class CubeEditor : MonoBehaviour
    {
        private const int gridSize = 10;

        private void Update()
        {
            if (!Application.isPlaying)
            {
                SnapToGrid();
            }
        }

        private void SnapToGrid()
        {
            Vector2Int snappedPosition = CalculateGridPosition();
            transform.position = new Vector3(
                snappedPosition.x * gridSize,
                0f,
                snappedPosition.y * gridSize
            );
        }

        internal Vector2Int CalculateGridPosition()
        {
            return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
            );
        }
    }
}
