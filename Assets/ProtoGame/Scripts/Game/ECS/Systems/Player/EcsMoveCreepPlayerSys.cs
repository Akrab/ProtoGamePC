using Leopotam.EcsLite;
using ProtoGame.Extensions;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public class EcsMoveCreepPlayerSys : IEcsInitSystem, IEcsRunSystem
    {

        private EcsFilter _filter;
        private EcsPool<EPlayer> _player;
        private EcsPool<EInputCreepEvent> _moveCreep;
        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayer>().Inc<EInputCreepEvent>().End();

            _player = systems.GetWorld().GetPool<EPlayer>();
            _moveCreep = systems.GetWorld().GetPool<EInputCreepEvent>();
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
