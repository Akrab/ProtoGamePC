using ProtoGame.Game.Actor;
using UnityEngine;
using UnityEngine.AI;

namespace ProtoGame.Game.Actors.Enemy
{
    public class MoveEnemyObj : BaseActorObj
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        protected override void SetComps()
        {
           // _navMeshAgent.
        }
    }
}
