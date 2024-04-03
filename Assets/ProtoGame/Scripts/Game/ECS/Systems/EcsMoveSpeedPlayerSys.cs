using Leopotam.EcsLite;
using ProtoGame.Extensions;

namespace ProtoGame.Game.ECS
{
    public class EcsMoveSpeedPlayerSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsFilter _filterMove;
        private EcsPool<EPlayerComp> _player;
        private EcsPool<EInputSpeedMoveEvent> _moveSpped;
        private EcsPool<EMovePlayerObjListener> _moveListener;
        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayerComp>().Inc<EInputSpeedMoveEvent>().End();

            _player = systems.GetWorld().GetPool<EPlayerComp>();
            _moveSpped = systems.GetWorld().GetPool<EInputSpeedMoveEvent>();
            _filterMove = systems.GetWorld().Filter<EMovePlayerObjListener>().End();

            _moveListener = systems.GetWorld().GetPool<EMovePlayerObjListener>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty())
                return;

            bool isRun = false;
            foreach (var entity in _filter)
            {
                ref var m = ref _moveSpped.Get(entity);

                isRun= m.isRun;
                _moveSpped.Del(entity);
            }

            foreach (var item in _filterMove)
            {
                ref var listenerComp = ref _moveListener.Get(item);
                if (listenerComp.runListener == null)
                    continue;
                listenerComp.runListener.SetIsRun(isRun);
            }

        }
    }
}
