using UnityEngine;

namespace Scripts.Cube
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CubeEditor))]
    internal class Waypoint : MonoBehaviour
    {
        public bool IsPlaceable { get; set; } = true;
        public Waypoint ExploredFrom { get; set; } = default;
        public bool IsExplored { get; set; } = false;
        public Vector2Int GridPosition { get; set; } = default;

        [SerializeField] TextMesh gridPositionTextMesh = default;
        private CubeEditor cubeEditor;

        private void Awake() => cubeEditor = GetComponent<CubeEditor>();
        private void Start() => GridPosition = cubeEditor.CalculateGridPosition();
        private void Update() => UpdateLabel();

        private void UpdateLabel()
        {
            GridPosition = cubeEditor.CalculateGridPosition();

            gameObject.name = ToString();
            gridPositionTextMesh.text = ToString();
            gridPositionTextMesh.color = (IsPlaceable) ? Color.green : Color.red;
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0) && IsPlaceable)
            {
                FindObjectOfType<Player.TurretFactory>().AddTurret(this);
            }
        }

        public override string ToString() => $"{GridPosition.x},{GridPosition.y}";
    }
}