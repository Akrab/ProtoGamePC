using Leopotam.EcsLite;
using ProtoGame.Assets.ProtoGame.Scripts.Game;
using UnityEngine;
using Zenject;

namespace ProtoGame.Game.Actor.Enemy
{

    public interface IEnemy
    {
        GameObject gameObject { get; }
    }

    public class EnemyView : MonoBehaviour, IEnemy
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform _weaponPoint;

        [Inject] private EcsWorld _world;

        [Inject]
        public void Initialize()
        {

            var es = GetComponentsInChildren<IEntitySet>();

            foreach (var item in es)
            {
                item.Setup();
                item.SetWorld(_world);
            }
        }
    }
}
