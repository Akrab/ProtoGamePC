using Leopotam.EcsLite;
using ProtoGame.Game.Actor.Player;
using ProtoGame.Game.ECS;
using ProtoGame.Game.World;
using ProtoGame.Infrastructure.Controllers;
using RSG;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class GameGState : IGState
    {
        [Inject] private EcsWorld _ecsWorld;
        [Inject] private IResourseService _resourseService;
        [Inject] private ICoroutineRunner _coroutineRunner;
        [Inject] private IEcsController _ecsController;

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

        private IPromise PrepareScene()
        {
            var promise = new Promise();

            var sceneInitManager = Object.FindObjectOfType<SceneInitManager>().GetComponent<ISceneInitManager>();

            promise
                .Then(() => _coroutineRunner.WaitEndOfFrames(2))
                .Then(() => sceneInitManager.Setup())
                .Then(() => PreparePlayer(sceneInitManager))
                .Then(() => PrepareEnemies(sceneInitManager))
                .Done(() => {
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
                    return playerInstance;
                })
                .Then(playerInstance =>
                {
                    sceneInitManager.GameCameraSmooth.SetTarget(playerInstance.transform, new Vector3(-32, 25,- 32));

                    return playerInstance;
                })
                .Then(playerInstance =>
                {
                    int entity = _ecsWorld.NewEntity();
                    EcsPool<EPlayerComp> pool = _ecsWorld.GetPool<EPlayerComp>();
                    ref EPlayerComp plComp = ref pool.Add(entity);
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
                       Object.Instantiate(enemy, point.transform.position, point.transform.rotation);
                    }

                    
                }).Done();
            promise.Resolve();
            return promise;
        }


        public void Exit()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
        }
    }
}
