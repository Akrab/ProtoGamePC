using ProtoGame.Infrastructure.Containers;
using ProtoGame.UI;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class PromoMenuGState : IGState
    {
        [Inject] private readonly UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<PromoForm>().Show();
        }

        public void Exit()
        {
            _uiContainer.GetForm<PromoForm>().Hide();
        }
    }
}
