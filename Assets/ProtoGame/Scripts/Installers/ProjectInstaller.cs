using Leopotam.EcsLite;
using ProtoGame.Infrastructure.Containers;
using ProtoGame.Infrastructure.Controllers;
using ProtoGame.Infrastructure.States;
using ProtoGame.Services;
using UnityEngine;
using Zenject;


namespace ProtoGame.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>, IInitializable
    {
        [SerializeField] private ScriptableObject[] _containers;
        [SerializeField] private InputController _inputController;

        private void InstallECS()
        {
            var world = new EcsWorld();
            Container.Bind<EcsWorld>().FromInstance(world).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EcsController>().AsSingle().NonLazy();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<PromoService>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RarityService>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UserService>().FromNew().AsSingle().NonLazy();
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
            
        }

        private void InstallGameStateMachine()
        {
            var gStateMachine = new GameStateMachine();
            Container.BindInterfacesTo<GameStateMachine>().FromInstance(gStateMachine).AsSingle().NonLazy();

            gStateMachine.AddState(Container.Instantiate<LoadingGState>());
            gStateMachine.AddState(Container.Instantiate<MainMenuGState>());
            gStateMachine.AddState(Container.Instantiate<PromoMenuGState>());
        }

        private void InstallContainers()
        {

            var cc = Container.Resolve<ConfigContainer>();
            foreach (var container in _containers)
                cc.Add(container);

        }
        private void StartApp()
        {
            Container.Resolve<IGameStateMachine>().EnterToState<MainMenuGState>();
        }

        public override void InstallBindings()
        {
            Container.Bind<ConfigContainer>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIContainer>().AsSingle().NonLazy();
            InstallECS();
            InstallGameStateMachine();
            InstallContainers();
            InstallServices();
            InstallControllers();
            Container.BindInterfacesAndSelfTo<ProjectInstaller>().FromInstance(this).AsSingle();

        }

        public void Initialize()
        {
            StartApp();
        }
    }
}
