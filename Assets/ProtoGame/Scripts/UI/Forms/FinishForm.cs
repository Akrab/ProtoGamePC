using ProtoGame.Extensions.UI;
using ProtoGame.Infrastructure;
using ProtoGame.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ProtoGame.UI
{
    public class FinishForm : BaseForm<FinishForm>
    {

        [SerializeField] private ButtonExt _btnRestart;
        [SerializeField] private ButtonExt _btnToMainMenu;
        [Inject] private IGameStateMachine _gameStateMachine;

        private void Awake()
        {
            _btnToMainMenu.onClick.AddListener(OnClickToMainMenu);
        }

        private void OnClickToMainMenu()
        {
            _gameStateMachine.EnterToState<MainMenuGState>();
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
        }

    }
}
