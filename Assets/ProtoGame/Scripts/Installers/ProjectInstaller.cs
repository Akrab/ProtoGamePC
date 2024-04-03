using Leopotam.EcsLite;
using ProtoGame.Game.Actor.Enemy;
using ProtoGame.Infrastructure.Containers;
using ProtoGame.Infrastructure.Controllers;
using ProtoGame.Infrastructure.Factory;
using ProtoGame.Infrastructure.States;
using ProtoGame.Services;
using RSG;
using UnityEngine;
using Zenject;


namespace ProtoGame.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>, IInitializable
    {
        [SerializeField] private ScriptableObject[] _containers;
        [SerializeField] private InputController _inputController;

        [SerializeField] private CoroutineRunner _coroutineRunner;

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
            Container.BindInterfacesAndSelfTo<ResourseService>().FromNew().AsSingle().NonLazy();
            
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        }

        private void InstallGameStateMachine()
        {
            var gStateMachine = new GameStateMachine();
            Container.BindInterfacesTo<GameStateMachine>().FromInstance(gStateMachine).AsSingle().NonLazy();

            gStateMachine.AddState(Container.Instantiate<GameGState>());
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

        private void InstallFabrics()
        {
            Container.BindFactory<Transform, IPromise<IEnemy>, EnemyFactory>().FromFactory<CustomEnemyFactory>().NonLazy();

        }


        private void StartApp()
        {
            Container.Resolve<IGameStateMachine>().EnterToState<MainMenuGState>();
        }

        public override void InstallBindings()
        {
            Container.Bind<ConfigContainer>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIContainer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle().NonLazy();
            
            InstallECS();
            InstallContainers();
            InstallServices();
            InstallControllers();
            InstallFabrics();
            InstallGameStateMachine();

            Container.BindInterfacesAndSelfTo<ProjectInstaller>().FromInstance(this).AsSingle();
      
        }

        public void Initialize()
        {
            StartApp();
        }
    }
}
