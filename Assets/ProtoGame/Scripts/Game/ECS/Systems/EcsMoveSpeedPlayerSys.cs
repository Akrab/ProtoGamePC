using Leopotam.EcsLite;
using ProtoGame.Extensions;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public class EcsMoveSpeedPlayerSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<EPlayerComp> _player;
        private EcsPool<EInputSpeedMoveComp> _moveSpped;
        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayerComp>().Inc<EInputSpeedMoveComp>().End();

            _player = systems.GetWorld().GetPool<EPlayerComp>();
            _moveSpped = systems.GetWorld().GetPool<EInputSpeedMoveComp>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty())
                return;

            foreach (var entity in _filter)
            {
                ref var m = ref _moveSpped.Get(entity);

                Debug.LogError("Fast " + m.isRun);
                _moveSpped.Del(entity);
            }
        }
    }
}
