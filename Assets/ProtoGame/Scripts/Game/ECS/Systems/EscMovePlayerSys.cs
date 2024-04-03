using Leopotam.EcsLite;
using ProtoGame.Extensions;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public class EscMovePlayerSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<EPlayerComp> _player;
        private EcsPool<EInputMoveComp> _move;


        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayerComp>().Inc<EInputMoveComp>().End();

            _player = systems.GetWorld().GetPool<EPlayerComp>();
            _move = systems.GetWorld().GetPool<EInputMoveComp>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty())
                return;


            foreach (var entity in _filter)
            {
                ref var m = ref _move.Get(entity);

                Debug.LogError("Direct " + m.direct);
                _move.Del(entity);
            }
          

        }
    }
}