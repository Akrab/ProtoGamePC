

using ProtoGame.Game.Actor.Player;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public struct EPlayerComp
    {
        public PlayerView playerView;
    }

    /// <summary>
    /// ���������
    /// </summary>
    public struct EInputMoveComp
    {
        public Vector3 direct;
    }

    /// <summary>
    /// ����� ��� ���
    /// </summary>
    public struct EInputSpeedMoveComp
    {
        public bool isRun;
    }

    /// <summary>
    /// �������������� / �������� 
    /// </summary>
    public struct EInputCreepComp
    {
        public bool isCreep;
    }

}