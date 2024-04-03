using ProtoGame.Game.Actor.Player;
using ProtoGame.Game.Weapons;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public struct EPlayerComp
    {
        public PlayerView playerView;
        public IWeapon weapon;
    }

    /// <summary>
    /// ���������
    /// </summary>
    public struct EInputMoveEvent
    {
        public Vector3 direct;
    }

    /// <summary>
    /// ����� ��� ���
    /// </summary>
    public struct EInputSpeedMoveEvent
    {
        public bool isRun;
    }

    /// <summary>
    /// �������������� / �������� 
    /// </summary>
    public struct EInputCreepEvent
    {
        public bool isCreep;
    }

    public struct EMovePlayerObjListener
    {
        public IInputDirectionListener listener;
        public IInputRunListener runListener;
    }

    public struct EPlayerFireEvent
    {
        public bool isFire;
    }

}