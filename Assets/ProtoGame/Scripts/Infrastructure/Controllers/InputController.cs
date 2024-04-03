using Leopotam.EcsLite;
using ProtoGame.Extensions;
using ProtoGame.Game.ECS;
using ProtoGame.Infrastructure.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProtoGame
{
    public class InputController : MonoBehaviour
    {

        private const string ACTION_MAP = "Game";
        private const string ACTION_MOVE = "Move";
        private const string ACTION_MOVE_MODE = "MoveMode";

        //Присесть/подкрадываться
        private const string ACTION_CREEP_MODE = "СreepMode";

        private const string ACTION_FIRE = "Fire";

        [Inject] private EcsWorld _ecsWorld;
        [Inject] private IEcsController _ecsController;

        private Vector3 _move = Vector3.zero;

        private EcsFilter _playerFilder;
        private EcsPool<EPlayerComp> _playerPool;
        private EcsPool<EInputMoveEvent> _movePool;
        private EcsPool<EInputCreepEvent> _creepPool;
        private EcsPool<EInputSpeedMoveEvent> _speedPool;

        private EcsPool<EPlayerFireEvent> _fireEvent;

        [Inject]
        private void Initialize()
        {
            var inputActions = GetComponent<PlayerInput>().actions;
            var mp = inputActions.FindActionMap(ACTION_MAP);
            var wasd = mp.FindAction(ACTION_MOVE);

            wasd.performed += InputMove;
            wasd.canceled += InputMove;

            var modeMode = mp.FindAction(ACTION_MOVE_MODE);


            modeMode.performed += InputMode;
            modeMode.canceled += InputModeCanceled;

            var modeCreepMode = mp.FindAction(ACTION_CREEP_MODE);

            modeCreepMode.performed += InputCreepMode;
            modeCreepMode.canceled += InputCreepModeCanceled;


            var fireAction = mp.FindAction(ACTION_FIRE);

            fireAction.performed += FireAction;
            fireAction.canceled += FireAction;

            _movePool = _ecsWorld.GetPool<EInputMoveEvent>();
            _creepPool = _ecsWorld.GetPool<EInputCreepEvent>();
            _speedPool = _ecsWorld.GetPool<EInputSpeedMoveEvent>();
            _fireEvent = _ecsWorld.GetPool<EPlayerFireEvent>();

            _playerFilder = _ecsWorld.Filter<EPlayerComp>().End();
        }

        private void InputMove(InputAction.CallbackContext context)
        {

            if (_ecsController.IsRunGame == false)
            {
                return;
            }
            var v = context.ReadValue<Vector2>();
            _move.x = v.x;
            _move.y = 0;
            _move.z = v.y;


            if (_playerFilder.IsEmpty()) return;


            foreach (var entity in _playerFilder)
            {
                if (_movePool.Has(entity))
                {
                    ref var e1 = ref _movePool.Get(entity);
                    e1.direct = _move;
                }
                else
                {
                    ref var e1 = ref _movePool.Add(entity);
                    e1.direct = _move;
                }

            }

        }

        private void SetRun(bool isRun)
        {

        
            foreach (var entity in _playerFilder)
            {
                if (_speedPool.Has(entity))
                {
                    ref var e1 = ref _speedPool.Get(entity);
                    e1.isRun = isRun;
                }
                else
                {
                    ref var e1 = ref _speedPool.Add(entity);
                    e1.isRun = isRun;
                }

            }
        }

        private void InputMode(InputAction.CallbackContext context)
        {
            if (_ecsController.IsRunGame == false)
                return;

            SetRun(true);

        }

        private void InputModeCanceled(InputAction.CallbackContext context)
        {
            if (_ecsController.IsRunGame == false)
                return;

            SetRun(false);
        }


        private void InputCreepMode(InputAction.CallbackContext context)
        {
            if (_ecsController.IsRunGame == false)
                return;
            SetCreep(true);
        }

        private void InputCreepModeCanceled(InputAction.CallbackContext context)
        {
            if (_ecsController.IsRunGame == false)
                return;
            SetCreep(false);
        }

        private void SetCreep(bool isCreep)
        {

            foreach (var entity in _playerFilder)
            {
                if (_creepPool.Has(entity))
                {
                    ref var e1 = ref _creepPool.Get(entity);
                    e1.isCreep = isCreep;
                }
                else
                {
                    ref var e1 = ref _creepPool.Add(entity);
                    e1.isCreep = isCreep;
                }

            }
        }


        private void FireAction(InputAction.CallbackContext context)
        {
            if (_ecsController.IsRunGame == false)
                return;
            SetFire(true);
        }

        private void SetFire(bool isFire)
        {
            foreach (var entity in _playerFilder)
            {
                if (_fireEvent.Has(entity))
                {
                    ref var e1 = ref _fireEvent.Get(entity);
                    e1.isFire = isFire;
                }
                else
                {
                    ref var e1 = ref _fireEvent.Add(entity);
                    e1.isFire = isFire;
                }

            }
        }
    }
}