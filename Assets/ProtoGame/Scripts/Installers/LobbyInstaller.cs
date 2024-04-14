using ProtoGame.Infrastructure.States;
using Zenject;

namespace ProtoGame.Infrastructure.Installers
{
    public class LobbyInstaller : MonoInstaller<LobbyInstaller>
    {
        [Inject] private IGameStateMachine _gameStateMachine;
        public override void InstallBindings()
        {
            _gameStateMachine.EnterToState<LoadingGState>();
        }
    }

}