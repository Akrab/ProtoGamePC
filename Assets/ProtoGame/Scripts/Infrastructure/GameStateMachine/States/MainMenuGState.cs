using ProtoGame.Infrastructure.Containers;
using ProtoGame.UI;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class MainMenuGState : IGState
    {
        [Inject] private UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<MainForm>().Show();
        }

        public void Exit()
        {
            _uiContainer.GetForm<MainForm>().Hide();
        }
    }
}
