using ProtoGame.Game.Actor.Enemy;
using RSG;
using UnityEngine;
using Zenject;

namespace ProtoGame.Infrastructure.Factory
{

    public class EnemyFactory : PlaceholderFactory<Transform, IPromise<IEnemy>>
    {
    }

    public class CustomEnemyFactory : IFactory<Transform, IPromise<IEnemy>>
    {
        [Inject] private IResourseService _resourseService;

        public IPromise<IEnemy> Create(Transform point)
        {
            Promise<IEnemy> promiseEnemy = new Promise<IEnemy>();

            var promise = new Promise();
            promise
                .Then(() => _resourseService.LoadEnemy())
                .Then(enemy =>
                {
                   return Object.Instantiate(enemy, point.transform.position, point.transform.rotation).GetComponent<IEnemy>();

                }).Then(e => {

                    promiseEnemy.Resolve(e);
                }).Done();
            promise.Resolve();

            return promiseEnemy;
        }
    }
}
