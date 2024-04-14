using ProtoGame.Infrastructure.Containers;
using ProtoGame.UI;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class FinishGState : IGState
    {
 
        [Inject] private readonly UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<FinishForm>().Show();
        }

        public void Exit()
        {
            _uiContainer.GetForm<FinishForm>().Hide();
        }
    }
}
