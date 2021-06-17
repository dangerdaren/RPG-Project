using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] Color waypointColor = Color.white;
        [SerializeField] float waypointGizmoRadius = .25f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWaypoint(i).position, waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i).position, GetWaypoint(j).position);

            }
        }

        private int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        private Transform GetWaypoint(int i)
        {
            return transform.GetChild(i);
        }
    }
}
