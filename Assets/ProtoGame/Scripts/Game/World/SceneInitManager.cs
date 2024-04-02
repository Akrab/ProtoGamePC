using ProtoGame.Game.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoGame.Game.World
{

    public interface ISceneInitManager
    {
        void Setup();
        PlayerSpawnerPoint GetPlayerSpawnPoint { get; }
        IReadOnlyCollection<EnemySpawnerPoint> GetEnemySpawnerPoints { get; }
    }

    public class SceneInitManager: BaseMonoBehaviour, ISceneInitManager
    {
        [SerializeField] private EnemySpawnersContainer _enemySpawnersContainer;
        [SerializeField] private PlayerSpawnerPoint _playerSpawnerPoint;

        public PlayerSpawnerPoint GetPlayerSpawnPoint => _playerSpawnerPoint;

        public IReadOnlyCollection<EnemySpawnerPoint> GetEnemySpawnerPoints => _enemySpawnersContainer.Points;

        protected override void SetupMB()
        {
            _enemySpawnersContainer.Setup();
            _playerSpawnerPoint.Setup();
        }
    }
}
