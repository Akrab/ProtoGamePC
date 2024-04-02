using System.Collections.Generic;
using UnityEngine;

namespace ProtoGame
{
    public class PatrolMap : MonoBehaviour
    {
        [SerializeField]
        private PatrolPoint[] _points;

        public IReadOnlyCollection<PatrolPoint> Points => _points;

#if UNITY_EDITOR
        [SerializeField] private Color _gizmoColor = Color.yellow;
#endif

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {

            if (_points == null || _points.Length == 0)
                return;

            Gizmos.color = _gizmoColor;

            for (int i = 0; i < _points.Length; i++)
            {
                Gizmos.color = _gizmoColor;
                if (i + 1 >= _points.Length)
                {
                    Gizmos.DrawLine(_points[i].transform.position, _points[0].transform.position);
                    return;
                };

                Gizmos.DrawLine(_points[i].transform.position, _points[i + 1].transform.position);
            }


        }
#endif
    }
}
