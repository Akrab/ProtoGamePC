using Leopotam.EcsLite;
using ProtoGame.Extensions;
using ProtoGame.Infrastructure;
using ProtoGame.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace ProtoGame.Game.ECS
{
    public class EcsFinishPlayerSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsFilter _filterMove;
        private EcsPool<EPlayerFinish> _finishPool;

        [Inject]
        private IGameStateMachine _gameStateMachine;

        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayerFinish>().End();
            _filterMove = systems.GetWorld().Filter<EMovePlayerObjListener>().End();

            _finishPool = systems.GetWorld().GetPool<EPlayerFinish>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty()) { return; }

            foreach (var entity in _filter)
            {
                ref var m = ref _finishPool.Get(entity);
                _finishPool.Del(entity);
            }

            var moveListener = systems.GetWorld().GetPool<EMovePlayerObjListener>();
            foreach (var item in _filterMove)
            {
                ref var listenerComp = ref moveListener.Get(item);
                if (listenerComp.listener == null)
                    continue;
                listenerComp.listener.SetDirection(Vector3.zero);
            }


            _gameStateMachine.EnterToState<FinishGState>();
        }
    }
}