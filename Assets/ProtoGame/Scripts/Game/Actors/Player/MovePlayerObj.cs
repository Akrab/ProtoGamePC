using ProtoGame.Extensions;
using ProtoGame.Game.ECS;
using UnityEngine;

namespace ProtoGame.Game.Actor.Player
{
    public interface IInputDirectionListener
    {
        void SetDirection(Vector3 direct);
    }
    public interface IInputRunListener
    {
        void SetIsRun(bool isFast);
    }

    public class MovePlayerObj : BaseActorObj, IInputDirectionListener, IInputRunListener
    {

        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _runSpeed = 1f;
        private Vector3 _direct = Vector3.zero;
        private bool _isFast = false;

        public void SetDirection(Vector3 direct)
        {
            _direct = direct;
        }

        private void FixedUpdate()
        {

            if (_direct == Vector3.zero) { return; }
            _rigidbody.MovePosition(transform.position + _direct.IsoVectorConvert() * Time.deltaTime * (_isFast ? _runSpeed : _speed)); //   
        }

        protected override void SetComps()
        {
            _ecsIndex = _ecsWorld.NewEntity();
            var pool = _ecsWorld.GetPool<EMovePlayerObjListener>();
            ref var c1 = ref pool.Add(_ecsIndex);
            c1.listener = this;
            c1.runListener = this;
        }

        public void SetIsRun(bool isFast)
        {
            _isFast = isFast;
        }
    }
}
