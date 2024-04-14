using Leopotam.EcsLite;
using ProtoGame.Game.Actor.Player;
using ProtoGame.Game.ECS;
using ProtoGame.Game.World;
using ProtoGame.Infrastructure.Controllers;
using ProtoGame.Infrastructure.Factory;
using RSG;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class GameGState : IGState
    {
        [Inject] private DiContainer _di;
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private IResourseService _resourseService;
        [Inject] private ICoroutineRunner _coroutineRunner;
        [Inject] private IEcsController _ecsController;
        [Inject] private EnemyFactory _customEnemyFactory;

        public void Enter(object data = null)
        {
            var waitPromise = new Promise();
            waitPromise
                .Then(LoadScene)
                .Then(PrepareScene)
                .Done(() =>
                {
                    _ecsController.IsRunGame = true;
                }, Er => { Debug.LogError(Er); });

            waitPromise.Resolve();
        }

        private IPromise LoadScene()
        {
            var promise = new Promise();
            SceneManager.LoadSceneAsync(CONSTANTS.GAME_SCENE, LoadSceneMode.Additive).completed += (s) =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
                promise.Resolve();

            };

            return promise;
        }

        private IPromise<ISceneInitManager> LoadSceneManager()
        {
            var promise = new Promise<ISceneInitManager>();

            var sceneInitManager = Object.FindObjectOfType<SceneInitManager>().GetComponent<ISceneInitManager>();

            _di.Inject(sceneInitManager);
            sceneInitManager.Setup();
            promise.Resolve(sceneInitManager);
            promise.Done();
            return promise;
        }

        private IPromise PrepareScene()
        {
            var promise = new Promise();

            promise
                .Then(() => _coroutineRunner.WaitEndOfFrames(2))
                .Then(LoadSceneManager)
                .Then(sceneInitManager => { PreparePlayer(sceneInitManager); return sceneInitManager; })
                .Then(sceneInitManager => PrepareEnemies(sceneInitManager))
                .Done(() =>
                {
                    Resources.UnloadUnusedAssets();
                });

            promise.Resolve();
            return promise;
        }

        private IPromise PreparePlayer(ISceneInitManager sceneInitManager)
        {

            var promise = new Promise();
            promise
                .Then(() => _resourseService.LoadPlayer())
                .Then(player =>
                {
                    var point = sceneInitManager.GetPlayerSpawnPoint;

                    var playerInstance = Object.Instantiate(player, point.transform.position, point.transform.rotation).GetComponent<PlayerView>();
                    _di.Inject(playerInstance);
                    return playerInstance;
                })
                .Then(playerInstance =>
                {
                    sceneInitManager.GameCameraSmooth.SetTarget(playerInstance.transform, new Vector3(-32, 25, -32));

                    return playerInstance;
                })
                .Then(playerInstance =>
                {
                    var entity = _ecsWorld.NewEntity();
                    var pool = _ecsWorld.GetPool<EPlayerComp>();
                    ref var plComp = ref pool.Add(entity);
                    plComp.playerView = playerInstance;
                })
                .Done();
            promise.Resolve();
            return promise;

        }

        private IPromise PrepareEnemies(ISceneInitManager sceneInitManager)
        {
            var promise = new Promise();
            promise
                .Then(() => _resourseService.LoadEnemy())
                .Then(enemy =>
                {
                    var points = sceneInitManager.GetEnemySpawnerPoints;

                    foreach (var point in points)
                    {
                        var p = _customEnemyFactory.Create(point.transform);
                        p.Then(enemy =>
                        {
                        }).Done();
                    }


                }).Done();
            promise.Resolve();
            return promise;
        }


        public void Exit()
        {
            _ecsController.IsRunGame = false;
            var ents = new int[_ecsWorld.GetEntitiesCount()];
            _ecsWorld.GetAllEntities(ref ents);
            for(int i = 0; i < ents.Length; i++)
                _ecsWorld.DelEntity(ents[i]);

          //  SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
        }
    }
}