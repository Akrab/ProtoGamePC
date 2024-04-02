using RSG;
using UnityEngine;

namespace ProtoGame.Services
{
    public class ResourseService : IResourseService
    {
        public IPromise<GameObject> LoadEnemy()
        {
            return LoadPrefab<GameObject>(string.Format (Constants.ENEMY_PREFAB_PATH, "A"));
        }

        public IPromise<GameObject> LoadPlayer()
        {
            return LoadPrefab<GameObject>( Constants.PLAYER_PREFAB_PATH);
        }

        public IPromise<T> LoadPrefab<T>(string path) where T : UnityEngine.Object
        {
            var obj = Resources.Load<T>(path);

            Promise<T> t = new Promise<T>();
            t.Resolve(obj);
            return t;
        }
    }
}