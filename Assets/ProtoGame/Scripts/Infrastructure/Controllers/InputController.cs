using Leopotam.EcsLite;
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

        //Пресесть/подкрадываться
        private const string ACTION_CREEP_MODE = "СreepMode";
        [Inject] private EcsWorld _ecsWorld;


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


        }

        private void InputMove(InputAction.CallbackContext context)
        {

            var v = context.ReadValue<Vector2>();

            

        }

        private void InputMode(InputAction.CallbackContext context)
        {
            //var pool = _ecsWorld.GetPool<EInputMode>();

        }

        private void InputModeCanceled(InputAction.CallbackContext context)
        {
 
        }


        private void InputCreepMode(InputAction.CallbackContext context)
        {

        }

        private void InputCreepModeCanceled(InputAction.CallbackContext context)
        {

        }
    }
}
