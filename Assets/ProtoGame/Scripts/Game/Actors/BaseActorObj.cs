using Leopotam.EcsLite;
using ProtoGame.Assets.ProtoGame.Scripts.Game;
using ProtoGame.Game.Infrastructure;
using UnityEngine;

namespace ProtoGame.Game.Actor
{
    public abstract class BaseActorObj : BaseMonoBehaviour, IEntitySet
    {
        protected Rigidbody _rigidbody;
        protected EcsWorld _ecsWorld;
        protected override void SetupMB()
        {
            _rigidbody = GetComponent<Rigidbody>(); 
        }

        public void SetWorld(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }


        protected abstract void SetComps();
    }
}
