using ProtoGame.Game.Actor.Player;
using ProtoGame.Game.ECS;
using UnityEngine;

namespace ProtoGame.Game.Actor
{
    public class RotateObj : BaseActorObj, IInputDirectionListener
    {
        [SerializeField] private float _rotationSpeed = 3f;
        private Vector3 _direction;
        private float _currentVelocity;

        public void SetDirection(Vector3 direct)
        {
            _direction = direct;
        }

        public void SetIsRun(bool isFast)
        {
            throw new System.NotImplementedException();
        }

        protected override void SetComps()
        {
            _ecsIndex = _ecsWorld.NewEntity();
            var pool = _ecsWorld.GetPool<EMovePlayerObjListener>();
            ref var c1 = ref pool.Add(_ecsIndex);
            c1.listener = this;
        }

        private void Update()
        {
            if (_direction == Vector3.zero)
                return;
            var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            targetAngle -= -45;
            targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref _currentVelocity, _rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0);
        }
    }
}
