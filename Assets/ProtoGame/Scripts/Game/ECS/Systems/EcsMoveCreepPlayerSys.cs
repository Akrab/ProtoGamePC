using Leopotam.EcsLite;
using ProtoGame.Extensions;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public class EcsMoveCreepPlayerSys : IEcsInitSystem, IEcsRunSystem
    {

        private EcsFilter _filter;
        private EcsPool<EPlayerComp> _player;
        private EcsPool<EInputCreepComp> _moveCreep;
        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayerComp>().Inc<EInputCreepComp>().End();

            _player = systems.GetWorld().GetPool<EPlayerComp>();
            _moveCreep = systems.GetWorld().GetPool<EInputCreepComp>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty())
                return;


            foreach (var entity in _filter)
            {
                ref var m = ref _moveCreep.Get(entity);

                Debug.LogError("Creep " + m.isCreep);
                _moveCreep.Del(entity);
            }
        }
    }
}
