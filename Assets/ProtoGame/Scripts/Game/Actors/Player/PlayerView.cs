using Leopotam.EcsLite;
using ProtoGame.Assets.ProtoGame.Scripts.Game;
using UnityEngine;
using Zenject;

namespace ProtoGame.Game.Actor.Player
{
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody _rb;
        [Inject] private EcsWorld _world;
        
        [Inject]
        public void Initialize()
        {
            _rb = GetComponent<Rigidbody>();

            var es = GetComponentsInChildren<IEntitySet>();

            foreach (var item in es)
            {
                item.Setup();
                item.SetWorld(_world);
            }
        }
    }
}
