

using ProtoGame.Game.Actor.Player;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public struct EPlayerComp
    {
        public PlayerView playerView;
    }

    /// <summary>
    /// Двигаемся
    /// </summary>
    public struct EInputMoveComp
    {
        public Vector3 direct;
    }

    /// <summary>
    /// бежим или как
    /// </summary>
    public struct EInputSpeedMoveComp
    {
        public bool isRun;
    }

    /// <summary>
    /// Подкрадываемся / крадемся 
    /// </summary>
    public struct EInputCreepComp
    {
        public bool isCreep;
    }

}