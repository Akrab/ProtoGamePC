using ProtoGame.Extensions.UI;
using ProtoGame.Infrastructure;
using ProtoGame.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace ProtoGame.UI
{
    public class MainForm : BaseForm<MainForm>
    {
        [SerializeField] private ButtonExt _btnStart;
        [Inject] private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            _btnStart.onClick.AddListener(ButtonStartClick);
        }

        private void ButtonStartClick()
        {
            _gameStateMachine.EnterToState<GameGState>();
        }
    }
}
