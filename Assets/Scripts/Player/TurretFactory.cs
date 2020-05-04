using Scripts.Cube;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    internal class TurretFactory : MonoBehaviour
    {
        [SerializeField] Turret turretPrefab;
        [SerializeField] Transform parent;
        [SerializeField] Text turretsPlacedText;
        [SerializeField] int turretLimit = 3;

        private Queue<Turret> turrets = new Queue<Turret>();

        private void Start()
        {
            UpdateTurretText();
        }

        public void AddTurret(Waypoint baseWaypoint)
        {
            if (turrets.Count < turretLimit)
            {
                Turret newTurret = CreateNewTurret(baseWaypoint);
                turrets.Enqueue(newTurret);
            }
            else
            {
                // Move existing tower
                Turret oldTurret = turrets.Dequeue();

                oldTurret.baseWaypoint.IsPlaceable = true;
                baseWaypoint.IsPlaceable = false;
                oldTurret.baseWaypoint = baseWaypoint;
                oldTurret.transform.position = GetPosition(baseWaypoint);

                turrets.Enqueue(oldTurret);
            }

            UpdateTurretText();
        }

        private Turret CreateNewTurret(Waypoint baseWaypoint)
        {
            Turret newTurret = Instantiate(turretPrefab, GetPosition(baseWaypoint), Quaternion.identity);

            newTurret.transform.parent = parent;
            newTurret.baseWaypoint = baseWaypoint;
            baseWaypoint.IsPlaceable = false;

            return newTurret;
        }

        private void UpdateTurretText() => turretsPlacedText.text = $"Turrets: {turrets.Count}/{turretLimit}";

        private Vector3 GetPosition(Waypoint baseWaypoint)
        {
            return new Vector3(baseWaypoint.transform.position.x,
                               baseWaypoint.GetComponent<BoxCollider>().size.y,
                               baseWaypoint.transform.position.z);
        }
    }
}
