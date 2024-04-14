using Leopotam.EcsLite;
using ProtoGame.Extensions;
using ProtoGame.Game.ECS;
using UnityEngine;

namespace ProtoGame.Game.World
{
    public class FinishPoint : MonoBehaviour
    {
        private EcsWorld _ecsWorld;

        private EcsPool<EPlayerFinish> _finishPool;
        private EcsFilter _filter;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(CONSTANTS.PLAYER_TAG))
                return;

            if (!_filter.IsEmpty())
                return;

            _finishPool.Add(_ecsWorld.NewEntity());

        }

        public void Initialize(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
            _finishPool = _ecsWorld.GetPool<EPlayerFinish>();
            _filter = _ecsWorld.Filter<EPlayerFinish>().End();

        }
    }
}
