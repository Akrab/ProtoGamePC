﻿using Leopotam.EcsLite;
using ProtoGame.Game.ECS;
using System;
using Zenject;

namespace ProtoGame.Infrastructure.Controllers
{

    public interface IEcsController
    {
        public bool IsInit { get; }
        public bool IsRunGame { get; set; }
    }

    public class EcsController : ITickable, IInitializable, IDisposable, IEcsController
    {

        [Inject] private readonly EcsWorld _ecsWorld;
        [Inject] private readonly DiContainer _container;

        private EcsSystems _updateSystem;

        public bool IsInit { get; private set; }
        public bool IsRunGame { get; set; }
        public void Dispose()
        {
            _updateSystem?.Destroy();
        }

        public void Initialize()
        {
            _updateSystem = new EcsSystems(_ecsWorld);


#if UNITY_EDITOR
            // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
            // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
            _updateSystem.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            // Регистрируем отладочные системы по контролю за текущей группой систем. 
            _updateSystem.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem());
#endif

            _updateSystem.Add(_container.Instantiate<EscMovePlayerSys>());
            _updateSystem.Add(_container.Instantiate<EcsMoveSpeedPlayerSys>());
            _updateSystem.Add(_container.Instantiate<EcsMoveCreepPlayerSys>());
            _updateSystem.Add(_container.Instantiate<EcsFinishPlayerSys>());

           
            //  _updateSystem.Inject();
            _updateSystem.Init();
            IsInit = true;
        }

        public void Tick()
        {
            if (!IsInit) return;
            if (!IsRunGame) return;
            _updateSystem?.Run();
        }


    }
}