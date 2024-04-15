using Leopotam.EcsLite;
using ProtoGame.Extensions;

namespace ProtoGame.Game.ECS
{
    public class EcsPlayerFireSys : IEcsInitSystem, IEcsRunSystem
    {

        private EcsFilter _filter;
        private EcsPool<EPlayer> _player;
        private EcsPool<EPlayerFireEvent> _firePool;

        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<EPlayer>().Inc<EPlayerFireEvent>().End();

            _player = systems.GetWorld().GetPool<EPlayer>();
            _firePool = systems.GetWorld().GetPool<EPlayerFireEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filter.IsEmpty())
                return;

            foreach (var entity in _filter)
            {
                ref var m = ref _firePool.Get(entity);
                Fire(entity, m.isFire);
                _firePool.Del(entity);
            }
        }

        private void Fire(int entity, bool isFire)
        {
            ref var m = ref _player.Get(entity);
            
        }
    }
}