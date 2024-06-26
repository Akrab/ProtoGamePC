﻿using Leopotam.EcsLite;
using ProtoGame.Game.ECS;
using ProtoGame.Game.Infrastructure;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ProtoGame.Game.World
{

    public interface ISceneInitManager
    {
        void Setup();
        PlayerSpawnerPoint GetPlayerSpawnPoint { get; }
        IReadOnlyCollection<EnemySpawnerPoint> GetEnemySpawnerPoints { get; }
        SmoothCameraFollow GameCameraSmooth {  get; }

    }

    public class SceneInitManager: BaseMonoBehaviour, ISceneInitManager
    {
        [SerializeField] private EnemySpawnersContainer _enemySpawnersContainer;
        [SerializeField] private PlayerSpawnerPoint _playerSpawnerPoint;

        [SerializeField] private SmoothCameraFollow _gameCameraSmooth;
        [SerializeField] private FinishPoint _finishPoint;

        [Inject] private EcsWorld _ecsWorld;

        public PlayerSpawnerPoint GetPlayerSpawnPoint => _playerSpawnerPoint;

        public IReadOnlyCollection<EnemySpawnerPoint> GetEnemySpawnerPoints => _enemySpawnersContainer.Points;

        public SmoothCameraFollow GameCameraSmooth => _gameCameraSmooth;


        [Inject]
        private void Initialize()
        {
            _finishPoint.Initialize(_ecsWorld);
            InitCamera();

        }

        private void InitCamera()
        {
            var entity = _ecsWorld.NewEntity();
            var pool = _ecsWorld.GetPool<EPlayerCamera>();
            ref var comp = ref pool.Add(entity);
            comp.gameObject = _gameCameraSmooth.gameObject;
            comp.transform = _gameCameraSmooth.transform;
        }

        protected override void SetupMB()
        {
            _enemySpawnersContainer.Setup();
            _playerSpawnerPoint.Setup();
        }

        
    }
}
