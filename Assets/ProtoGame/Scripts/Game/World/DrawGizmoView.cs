

using UnityEngine;

public class DrawGizmoView : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField] private float _gizmoRadius = 1f;
    [SerializeField] private Color _gizmoColor  = Color.yellow;
#endif

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawWireSphere(transform.position, _gizmoRadius);
    }
#endif
}