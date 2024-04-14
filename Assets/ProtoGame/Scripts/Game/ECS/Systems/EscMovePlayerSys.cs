using Leopotam.EcsLite;
using ProtoGame.Extensions;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public class EscMovePlayerSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<EPlayerComp> _player;
        private EcsPool<EInputMoveEvent> _move;

        private EcsFilter _filterMove;
        private EcsPool<EMovePlayerObjListener> _moveListener;
        private Vector3 _direct = Vector3.zero;

        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayerComp>().Inc<EInputMoveEvent>().End();

            _filterMove = systems.GetWorld().Filter<EMovePlayerObjListener>().End();

            _player = systems.GetWorld().GetPool<EPlayerComp>();
            _move = systems.GetWorld().GetPool<EInputMoveEvent>();

            _moveListener = systems.GetWorld().GetPool<EMovePlayerObjListener>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty())
                return;


         
            foreach (var entity in _filter)
            {
                ref var m = ref _move.Get(entity);
                _direct = m.direct;

                _move.Del(entity);
            }


            foreach (var item in _filterMove)
            {
                ref var listenerComp = ref _moveListener.Get(item);
                if (listenerComp.listener == null)
                    continue;
                listenerComp.listener.SetDirection(_direct);
            }


        }
    }
}