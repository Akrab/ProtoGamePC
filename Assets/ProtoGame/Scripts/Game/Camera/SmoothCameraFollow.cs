using ProtoGame.Game.Infrastructure;
using UnityEngine;

namespace ProtoGame.Game
{
    public class SmoothCameraFollow: BaseMonoBehaviour
    {

        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _target;
        [SerializeField] private float _smooth;
        [SerializeField] private float Y_Border = -100f;

        private Vector3 m_currentVelocity = Vector3.zero;
        private Vector3 m_targetPostion = Vector3.zero;

        public void SetTarget(Transform target, Vector3 offset)
        {
            _target = target;
            _offset = offset;
        }

        private void LateUpdate()
        {
            if (!_target)
                return;
            m_targetPostion = _target.position + _offset;
            if (m_targetPostion.y <= Y_Border)
                m_targetPostion.y = Y_Border;
            transform.position = Vector3.SmoothDamp(transform.position, m_targetPostion, ref m_currentVelocity, _smooth);

        }
    }
}
